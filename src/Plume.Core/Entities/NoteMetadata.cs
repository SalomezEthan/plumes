namespace Plume.Core.Entities;

public class NoteMetadata
{
    public const int MAX_NAME_LENGTH = 24;

    public NoteMetadata(Guid id, string name)
    {
        Id = id;
        Name = name;
    }

    public Guid Id { get; }
    public string Name { get; set; }
}