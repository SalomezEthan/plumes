namespace Plume.Core.UseCases.Note.Create;

public sealed record CreateNoteCommand(
    string Name,
    Guid DirectoryId
);