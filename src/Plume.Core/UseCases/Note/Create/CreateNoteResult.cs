namespace Plume.Core.UseCases.Note.Create;

public sealed record CreateNoteResult(
    Guid CreatedNoteId,
    string CreatedNoteName
);