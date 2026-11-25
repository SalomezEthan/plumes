namespace Plume.Core.Tests.Gateways.Impl;

public sealed class FakeNoteRepository : INoteRepository
{
    public bool DeleteCalled { get; private set; }

    public async Task DeleteAsync(Guid noteId)
    {
        DeleteCalled = true;
    }
}