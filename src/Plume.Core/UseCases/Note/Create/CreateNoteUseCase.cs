using Plume.Core.Entities;
using Plume.Core.Exceptions;
using Plume.Core.Gateways;
using Plume.Core.ValueObjects;

namespace Plume.Core.UseCases.Note.Create;

public sealed class CreateNoteUseCase(IDirectoryRepository dirRepo)
{
    public async Task<CreateNoteResult> ExecuteAsync(CreateNoteCommand command)
    {
        var noteName = new NoteName(command.Name);

        var dir = await dirRepo.GetById(command.DirectoryId) 
        ?? throw new NotFoundException("Dossier", command.DirectoryId);

        if (dir.Children.Any(child => child.Name.ToString().Equals(noteName.ToString(), StringComparison.CurrentCulture)))
        {
            throw new NameAlreadyExistsException();
        }

        var metadata = new NoteMetadata(
            id: Guid.NewGuid(),
            name : noteName
        );

        dir.Children.Add(metadata);

        await dirRepo.SaveAsync(dir);

        return new CreateNoteResult(
            CreatedNoteId: metadata.Id,
            CreatedNoteName: metadata.Name.ToString()
        );
    }
}