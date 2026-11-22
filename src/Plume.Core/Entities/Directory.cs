using Plume.Core.Exceptions;
using Plume.Core.ValueObjects;

namespace Plume.Core.Entities;

public class Directory
{
    private List<NoteMetadata> _children = [];

    public Directory(Guid id, IEnumerable<NoteMetadata> children)
    {
        Id = id;
        
        _children = [.. children];
    }

    public Guid Id { get; }
    public IReadOnlyList<NoteMetadata> Children => _children;

    public void AddChild(NoteMetadata newNote)
    {
        if (_children.Any(note => note.Name.Equals(newNote.Name)))
        {
            throw new NameAlreadyExistsException();
        }

        _children.Add(newNote);
    }
}