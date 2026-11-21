namespace Plume.Core.Entities;

public class Directory
{

    public Directory(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; }
    public List<NoteMetadata> Children { get; } = [];
}