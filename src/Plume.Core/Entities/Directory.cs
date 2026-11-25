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

    public void RenameNote(Guid noteId, NoteName newName)
    {
        var note = _children.First(note => note.Id == noteId);

        if (note.Name.Equals(newName))
        {
            return;
        }

        if (_children.Any(note => note.Name.Equals(newName)))
        {
            throw new NameAlreadyExistsException();
        }

        note.SetName(newName);
        
        _children.Remove(note);
        _children.Add(note);
    }
}