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
    public List<Core.Entities.Directory> Items { get; } = [];

    public Task<Core.Entities.Directory?> GetByIdAsync(Guid directoryId)
    {
        if (FindsNothing)
        {
            return Task.FromResult<Core.Entities.Directory?>(null);
        }

        var dir = new Core.Entities.Directory(
            id: directoryId,
            children: []
        );

        if (_defaultChildName is not null)
        {
            dir.AddChild(new NoteMetadata(Guid.NewGuid(), _defaultChildName));
        }
        
        return Task.FromResult<Core.Entities.Directory?>(dir);
    }

    public async Task SaveAsync(Core.Entities.Directory dir)
    {
        Items.Add(dir);
    }

    public void PrepareGeneratedFolderWithMetadataWithName(NoteName name)
    {
        _defaultChildName = name;
    }
}