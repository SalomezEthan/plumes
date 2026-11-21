using Plume.Core.Entities;
using Plume.Core.Exceptions;
using Plume.Core.UseCases.Note.Create;
using Plume.Core.Tests.Gateways.Impl;

namespace Plume.Core.Tests.UseCases.Note;

[TestClass]
public class CreateNoteUseCaseTests
{
    private const string DEFAULT_NAME = "une-note";

    private FakeDirectoryRepository _dirRepo;
    private CreateNoteUseCase _uc;

    public CreateNoteUseCaseTests()
    {
        _dirRepo = new FakeDirectoryRepository();
        _uc = new CreateNoteUseCase(_dirRepo);
    }

    [TestMethod]
    public async Task ShouldCreateNote()
    {
        var command = new CreateNoteCommand(
            Name: DEFAULT_NAME,
            DirectoryId: Guid.NewGuid()
        );

        var response = await _uc.ExecuteAsync(command);

        Assert.IsNotNull(_dirRepo.Items.FirstOrDefault());
        Assert.AreEqual(DEFAULT_NAME, _dirRepo.Items.First().Children[0].Name);
    }

    [TestMethod]
    public async Task ShouldThrowWhenNameIsEmpty()
    {
        var command = new CreateNoteCommand(
            Name: string.Empty,
            DirectoryId: Guid.NewGuid()
        );

        await Assert.ThrowsExactlyAsync<ArgumentException>(() => _uc.ExecuteAsync(command));
    }

    [TestMethod]
    public async Task ShouldThrowWhenNameIsTooLong()
    {
        var command = new CreateNoteCommand(
            Name: new string('c', NoteMetadata.MAX_NAME_LENGTH + 1),
            DirectoryId: Guid.NewGuid()
        );

        await Assert.ThrowsExactlyAsync<NameTooLongException>(() => _uc.ExecuteAsync(command));
    }

    [TestMethod]
    public async Task ShouldThrowWhenNameAlreadyExists()
    {
        var command = new CreateNoteCommand(
            Name: DEFAULT_NAME,
            DirectoryId: Guid.NewGuid()
        );

        _dirRepo.PrepareGeneratedFolderWithMetadataWithName(command.Name);
        await Assert.ThrowsExactlyAsync<NameAlreadyExistsException>(() => _uc.ExecuteAsync(command));
    }

    [TestMethod]
    public async Task ShouldThrowWhenDirectoryNotExists()
    {
        var command = new CreateNoteCommand(
            Name: DEFAULT_NAME,
            DirectoryId: Guid.NewGuid()
        );

        _dirRepo.FindsNothing = true;
        await Assert.ThrowsExactlyAsync<NotFoundException>(() => _uc.ExecuteAsync(command));
    }
}