namespace Plume.Core.UseCases.Note.Rename;

public sealed record RenameNoteResult(
    Guid NoteId,
    string NewName
);