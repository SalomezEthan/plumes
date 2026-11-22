using Plume.Core.Entities;
using Plume.Core.Exceptions;
using Plume.Core.UseCases.Note.Create;
using Plume.Core.Tests.Gateways.Impl;
using Plume.Core.ValueObjects;

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
        Assert.AreEqual(DEFAULT_NAME, _dirRepo.Items.First().Children[0].Name.ToString());
    }

    [TestMethod]
    public async Task ShouldThrowWhenNameAlreadyExists()
    {
        var command = new CreateNoteCommand(
            Name: DEFAULT_NAME,
            DirectoryId: Guid.NewGuid()
        );

        _dirRepo.PrepareGeneratedFolderWithMetadataWithName(new NoteName(command.Name));
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