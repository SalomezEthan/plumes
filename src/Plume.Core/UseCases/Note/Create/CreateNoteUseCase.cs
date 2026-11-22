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

        var metadata = new NoteMetadata(
            id: Guid.NewGuid(),
            name : noteName
        );

        var dir = await dirRepo.GetByIdAsync(command.DirectoryId) 
        ?? throw new NotFoundException("Dossier", command.DirectoryId);

        dir.AddChild(metadata);

        await dirRepo.SaveAsync(dir);

        return new CreateNoteResult(
            CreatedNoteId: metadata.Id,
            CreatedNoteName: metadata.Name.ToString()
        );
    }
}