using Plume.Core.Tests.Gateways.Impl;
using Plume.Core.ValueObjects;

namespace Plume.Core.Tests.Gateways.Tests;

/*
* 
*/

[TestClass]
public class FakeDirectoryRepositoryTest
{
    private readonly FakeDirectoryRepository _repo = new();

    [TestMethod]
    public async Task PrepareGeneratedFolderWithMetadataWithName_ShouldReturnDirWithFirstDirChildWithName() 
    {
        NoteName name = new ("cool");
        
        _repo.PrepareGeneratedFolderWithMetadataWithName(name);
        var dir = await _repo.GetById(Guid.NewGuid());

        Assert.AreEqual(name, dir!.Children[0].Name);
    }

    [TestMethod]
    public async Task GetById_ShouldReturnGeneratedDir()
    {
        var dir = await _repo.GetById(Guid.NewGuid());
        Assert.IsNotNull(dir);
    }

    [TestMethod]
    public async Task FindsNothing_ShouldChangeGetResultByNull()
    {
        _repo.FindsNothing = true;
        var dir = await _repo.GetById(Guid.NewGuid());

        Assert.IsNull(dir);
    }

    [TestMethod]
    public async Task Save_ShouldAddDirInPersistenceList()
    {
        var id = Guid.NewGuid();
        var dir = new Entities.Directory(id);
        await _repo.SaveAsync(dir);
        Assert.IsTrue(_repo.Items.Any(dir => dir.Id == id));
    }
}