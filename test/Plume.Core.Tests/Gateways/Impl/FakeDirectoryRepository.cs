using Plume.Core.Entities;
using Plume.Core.Gateways;

namespace Plume.Core.Tests.Gateways.Impl;

public class FakeDirectoryRepository : IDirectoryRepository
{
    private string? _defaultChildName;

    public FakeDirectoryRepository()
    {
    }

    public bool FindsNothing { get; set; }
    public List<Entities.Directory> Items { get; } = [];

    public Task<Entities.Directory?> GetById(Guid directoryId)
    {
        if (FindsNothing)
        {
            return Task.FromResult<Entities.Directory?>(null);
        }

        var dir = new Entities.Directory(directoryId);

        if (_defaultChildName is not null)
        {
            dir.Children.Add(new NoteMetadata(Guid.NewGuid(), _defaultChildName ?? "thomas"));
        }
        
        return Task.FromResult<Entities.Directory?>(dir);
    }

    public async Task SaveAsync(Entities.Directory dir)
    {
        Items.Add(dir);
    }

    public void PrepareGeneratedFolderWithMetadataWithName(string name)
    {
        _defaultChildName = name;
    }
}