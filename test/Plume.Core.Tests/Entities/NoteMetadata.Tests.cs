using Plume.Core.Entities;
using Plume.Core.ValueObjects;

namespace Plume.Core.Tests.Entities;

[TestClass]
public class NoteMetadataTests
{
    public void SetName_ShouldSetName()
    {
        var firstName = new NoteName("file");
        var secondName = new NoteName("folder");

        var note = new NoteMetadata(
            Guid.NewGuid(),
            firstName
            );

        note.SetName(secondName);

        Assert.AreEqual(secondName, note.Name);
    }
}