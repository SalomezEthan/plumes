using Plume.Core.Entities;
using Plume.Core.Exceptions;
using Plume.Core.Tests.Gateways.Impl;
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

        string newName = "nouveau-nom";
        var cmd = new RenameNoteCommand(
            noteId: Guid.NewGuid(),
            newName: newName
        );

        await _uc.ExecuteAsync(cmd);

        var nameInPersistence = _dirRepo
            .Items[0]
            .Children[0]
            .Name;

        Assert.AreNotEqual(name, nameInPersistence);
        Assert.AreEqual(nameInPersistence.ToString(), newName);
    }

    [TestMethod]
    public async Task ShouldReturnResult()
    {
        var name = new NoteName("file");
        _dirRepo.PrepareGeneratedFolderWithMetadataWithName(name);

        string newName = "nouveau-nom";
        var cmd = new RenameNoteCommand(
            noteId: Guid.NewGuid(),
            newName: newName
        );

        var result = await _uc.ExecuteAsync(cmd);

        Assert.AreEqual(cmd.noteId, result.NoteId);
        Assert.AreEqual(newName, result.NewName);
    }

    [TestMethod]
    public async Task ShouldThrow_WhenParentDirIsNotFound()
    {
        _dirRepo.FindsNothing = true;

        var cmd = new RenameNoteCommand(
            noteId: Guid.NewGuid(),
            newName: "coucou"
        );

        await Assert.ThrowsExactlyAsync<NotFoundException>(() => _uc.ExecuteAsync(cmd));
    }
}