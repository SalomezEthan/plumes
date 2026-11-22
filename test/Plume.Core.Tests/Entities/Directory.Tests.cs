using Plume.Core.Entities;
using Plume.Core.Exceptions;
using Plume.Core.ValueObjects;

namespace Plume.Core.Tests.Entities;

[TestClass]
public class DirectoryTests
{

    [TestMethod]
    public void Constructor_ShouldSetChildrenList()
    {
        var note = new NoteMetadata(
            id: Guid.NewGuid(),
            name: new NoteName("file")
        );

        var dir = new Core.Entities.Directory(
            id: Guid.NewGuid(),
            children: [note]
        );

        Assert.Contains(note, dir.Children);
    }

    [TestMethod]
    public void AddChild_ShouldAddChild()
    {
        var dir = new Core.Entities.Directory(
            id: Guid.NewGuid(), 
            children: []
            );

        var note = new NoteMetadata(
            id: Guid.NewGuid(),
            name: new NoteName("file")
        );

        dir.AddChild(note);
        Assert.Contains(note, dir.Children);
    }

    [TestMethod]
    public void AddChild_ShouldThrow_WhenChildNameAlreadyExistsInDir()
    {
        string dupplicatedName = "other";

        var existingNote = new NoteMetadata(
            id: Guid.NewGuid(), 
            name: new NoteName(dupplicatedName)
            );

        var newNote = new NoteMetadata(
            id: Guid.NewGuid(), 
            name: new NoteName(dupplicatedName)
            );

        var dir = new Core.Entities.Directory(
            id: Guid.NewGuid(), 
            children: [existingNote]
            );

        Assert.ThrowsExactly<NameAlreadyExistsException>(() => dir.AddChild(newNote));
    }
}