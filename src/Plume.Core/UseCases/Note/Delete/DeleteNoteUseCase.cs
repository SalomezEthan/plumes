using Plume.Core.Gateways;

namespace Plume.Core.UseCases.Note.Delete;

public sealed class DeleteNoteUseCase(INoteRepository repo)
{
    public async Task<DeleteNoteResult> ExecuteAsync(DeleteNoteCommand command)
    {
        await repo.DeleteAsync(command.NoteId);
        return new DeleteNoteResult(
            command.NoteId
        );
    }
}