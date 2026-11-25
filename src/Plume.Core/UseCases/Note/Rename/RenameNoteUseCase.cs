using Plume.Core.Exceptions;
using Plume.Core.Gateways;
using Plume.Core.ValueObjects;

namespace Plume.Core.UseCases.Note.Rename;

public sealed class RenameNoteUseCase(IDirectoryRepository repo)
{
    public async Task<RenameNoteResult> ExecuteAsync(RenameNoteCommand command)
    {
        var newNoteName = new NoteName(command.NewName);

        var dir = await repo.GetNoteParentAsync(command.NoteId)
        ?? throw new NotFoundException("Note", command.NoteId);

        dir.RenameNote(command.NoteId, newNoteName);

        await repo.SaveAsync(dir);

        return new RenameNoteResult(
            command.NoteId,
            newNoteName.ToString()
        );
    }
}