using Plume.Core.ValueObjects;

namespace Plume.Core.Entities;

public class NoteMetadata
{

    public NoteMetadata(Guid id, NoteName name)
    {
        Id = id;
        Name = name;
    }

    public Guid Id { get; }
    public NoteName Name { get; private set; }
}