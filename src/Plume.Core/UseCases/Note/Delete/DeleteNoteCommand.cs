namespace Plume.Core.UseCases.Note.Delete;

public sealed record DeleteNoteCommand(
    Guid NoteId
);