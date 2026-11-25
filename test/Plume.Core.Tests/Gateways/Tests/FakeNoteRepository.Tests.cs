using Plume.Core.Tests.Gateways.Impl;

namespace Plume.Core.Tests.Gateways.Tests;

[TestClass]
public sealed class FakeNoteRepositoryTests
{
    private FakeNoteRepository _repo = new();

    [TestMethod]
    public async Task DeleteNote_ShouldSetDeleteCalledPropertyToTrue()
    {
        await _repo.DeleteAsync(Guid.NewGuid());
        Assert.IsTrue(_repo.DeleteCalled);
    }
}