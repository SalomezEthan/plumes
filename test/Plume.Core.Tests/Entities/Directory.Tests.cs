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

    [TestMethod]
    public void RenameNote_ShouldRenameNote()
    {
        NoteName oldName = new ("file");
        NoteName newName = new ("other");

        var renamedNote = new NoteMetadata(
            id: Guid.NewGuid(),
            name: oldName
        );

        var dir = new Core.Entities.Directory(
            id: Guid.NewGuid(),
            [renamedNote]
        );

        dir.RenameNote(renamedNote.Id, newName);

        Assert.AreNotEqual(oldName, renamedNote.Name);
        Assert.AreEqual(newName, renamedNote.Name);
    }

    [TestMethod]
    public void RenameNote_ShouldThrow_WhenNameAlreadyExistsInDir()
    {
        var dupplicatedName = new NoteName("file");

        var existedNote = new NoteMetadata(
            id: Guid.NewGuid(),
            name: dupplicatedName
        );

        var renamedNote = new NoteMetadata(
            id: Guid.NewGuid(),
            name: new NoteName("other")
        );

        var dir = new Core.Entities.Directory(
            id: Guid.NewGuid(),
            [existedNote, renamedNote]
        );

        Assert.ThrowsExactly<NameAlreadyExistsException>(() => dir.RenameNote(renamedNote.Id, dupplicatedName);)
    }

    public void RenameNote_ShouldIgnore_WhenNoteNameIsSame()
    {
        var existedNoteName = new NoteName("file");

        var existedNote = new NoteMetadata(
            id: Guid.NewGuid(),
            name: existedNoteName
        );

        var otherNote = new NoteMetadata(
            id: Guid.NewGuid(),
            name: new NoteName("other")
        );

        var dir = new Core.Entities.Directory(
            id: Guid.NewGuid(),
            [existedNote, otherNote]
        );

        dir.RenameNote(existedNote.Id, existedNoteName);
    }
}