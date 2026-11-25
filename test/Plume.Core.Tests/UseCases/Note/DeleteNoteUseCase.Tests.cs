using Plume.Core.Tests.Gateways.Impl;
using Plume.Core.UseCases.Note.Delete;

namespace Plume.Core.Tests.UseCases.Note;

[TestClass]
public class DeleteNoteUseCaseTests()
{
    [TestMethod]
    public async Task ShouldDeleteNote()
    {
        var repo = new FakeNoteRepository();
        var uc = new DeleteNoteUseCase(repo);

        var cmd = new DeleteNoteCommand(
            NoteId: Guid.NewGuid()
        );

        var result = await uc.ExecuteAsync(cmd);

        Assert.AreEqual(cmd.NoteId, result.DeletedNoteId);
        Assert.IsTrue(repo.DeleteCalled);
    }
}