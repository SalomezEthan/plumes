using Plume.Core.Exceptions;
using Plume.Core.Tests.Gateways.Impl;
using Plume.Core.UseCases.Note.Rename;
using Plume.Core.ValueObjects;

namespace Plume.Core.Tests.UseCases.Note;

[TestClass]
public class RenameNoteUseCaseTests
{
    private FakeDirectoryRepository _dirRepo;
    private RenameNoteUseCase _uc;

    public RenameNoteUseCaseTests()
    {
        _dirRepo = new FakeDirectoryRepository();
        _uc = new RenameNoteUseCase(_dirRepo);
    }

    [TestMethod]
    public async Task ShouldRenameNote()
    {
        var name = new NoteName("file");
        _dirRepo.PrepareGeneratedFolderWithMetadataWithName(name);

        string NewName = "nouveau-nom";
        var cmd = new RenameNoteCommand(
            NoteId: Guid.NewGuid(),
            NewName: NewName
        );

        await _uc.ExecuteAsync(cmd);

        Assert.IsTrue(_dirRepo.SaveCalled);
    }

    [TestMethod]
    public async Task ShouldReturnResult()
    {
        var name = new NoteName("file");
        _dirRepo.PrepareGeneratedFolderWithMetadataWithName(name);

        string NewName = "nouveau-nom";
        var cmd = new RenameNoteCommand(
            NoteId: Guid.NewGuid(),
            NewName: NewName
        );

        var result = await _uc.ExecuteAsync(cmd);

        Assert.AreEqual(cmd.NoteId, result.NoteId);
        Assert.AreEqual(NewName, result.NewName);
    }

    [TestMethod]
    public async Task ShouldThrow_WhenParentDirIsNotFound()
    {
        _dirRepo.FindsNothing = true;

        var cmd = new RenameNoteCommand(
            NoteId: Guid.NewGuid(),
            NewName: "coucou"
        );

        await Assert.ThrowsExactlyAsync<NotFoundException>(() => _uc.ExecuteAsync(cmd));
    }
}