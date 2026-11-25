using Plume.Core.Entities;
using Plume.Core.Gateways;
using Plume.Core.ValueObjects;

namespace Plume.Core.Tests.Gateways.Impl;

public class FakeDirectoryRepository : IDirectoryRepository
{
    private NoteName? _defaultChildName;

    public FakeDirectoryRepository()
    {
    }

    public bool FindsNothing { get; set; }
    public bool SaveCalled { get; private set; }
    public List<Core.Entities.Directory> Items { get; } = [];

    public async Task<Core.Entities.Directory?> GetByIdAsync(Guid directoryId)
    {
        if (FindsNothing)
        {
            return null;
        }

        var dir = new Core.Entities.Directory(
            id: directoryId,
            children: []
        );

        if (_defaultChildName is not null)
        {
            dir.AddChild(new NoteMetadata(Guid.NewGuid(), _defaultChildName));
        }
        
        return dir;
    }

    public async Task SaveAsync(Core.Entities.Directory dir)
    {
        Items.Add(dir);
        SaveCalled = true;
    }

    public void PrepareGeneratedFolderWithMetadataWithName(NoteName name)
    {
        _defaultChildName = name;
    }

    public async Task<Core.Entities.Directory?> GetNoteParentAsync(Guid noteId)
    {
        if (FindsNothing)
        {
            return null;
        }

        var note = new NoteMetadata(
            noteId,
            new NoteName("file")
        );

        return new Core.Entities.Directory(
            id: Guid.NewGuid(),
            children: [note]
        );
    }
}