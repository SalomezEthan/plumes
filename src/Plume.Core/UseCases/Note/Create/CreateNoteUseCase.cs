using Plume.Core.Entities;
using Plume.Core.Exceptions;
using Plume.Core.Gateways;

namespace Plume.Core.UseCases.Note.Create;

public sealed class CreateNoteUseCase(IDirectoryRepository dirRepo)
{
    public async Task<CreateNoteResult> ExecuteAsync(CreateNoteCommand command)
    {
        string name = command.Name.Trim();

        if (name.Length == 0)
        {
            throw new ArgumentException("Le nom d'une note ne peut pas être vide.");
        }

        if (name.Length > NoteMetadata.MAX_NAME_LENGTH)
        {
            throw new NameTooLongException(NoteMetadata.MAX_NAME_LENGTH);
        }

        var dir = await dirRepo.GetById(command.DirectoryId) 
        ?? throw new NotFoundException("Dossier", command.DirectoryId);

        if (dir.Children.Any(child => child.Name.Equals(name, StringComparison.CurrentCulture)))
        {
            throw new NameAlreadyExistsException();
        }

        var metadata = new NoteMetadata(
            id: Guid.NewGuid(),
            name : name
        );

        dir.Children.Add(metadata);

        await dirRepo.SaveAsync(dir);

        return new CreateNoteResult(
            CreatedNoteId: metadata.Id,
            CreatedNoteName: metadata.Name
        );
    }
}