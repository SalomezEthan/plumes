namespace Plume.Core.UseCases.Note.Rename;

public sealed record RenameNoteCommand(
    Guid NoteId,
    string NewName
);