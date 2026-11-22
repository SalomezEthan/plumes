using Plume.Core.Exceptions;
using Plume.Core.ValueObjects;

namespace Plume.Core.Tests.ValueObjects;

[TestClass]
public class NoteNameTests
{
    public const string DEFAULT_NAME_VALUE = "mon-fichier";

    [TestMethod]
    public void Constructor_ShouldThrow_WhenNameIsEmpty()
    {
        string emptyName = string.Empty;
        Assert.ThrowsExactly<ArgumentException>(() => new NoteName(emptyName));    
    }

    [TestMethod]
    public void Constructor_ShouldThrow_WhenNameIsTooLong()
    {
        string nameTooLong = new string('c', NoteName.MAX_NAME_LENGTH + 1);
        Assert.ThrowsExactly<NameTooLongException>(() => new NoteName(nameTooLong));
    }

    [TestMethod]
    public void Constructor_ShouldThrow_WhenNameIsWhiteSpace()
    {
        string whiteSpaceName = "     ";
        Assert.ThrowsExactly<ArgumentException>(() => new NoteName(whiteSpaceName));
    }

    [TestMethod]
    public void Constructor_ShouldTrim_WhenNameHasWhiteSpace()
    {
        string name = "    mon-file";
        Assert.AreEqual(name.Trim(), new NoteName(name).ToString());
    }

    [TestMethod]
    public void ToString_ShouldReturnName()
    {
        string name = "mon-fichier";
        Assert.AreEqual(name, new NoteName(name).ToString());
    }

    [TestMethod]
    public void Equals_ShouldCompareNameValues()
    {
        var nameA = new NoteName(DEFAULT_NAME_VALUE);
        var nameB = new NoteName(DEFAULT_NAME_VALUE);

        Assert.AreEqual(nameA, nameB);
    }

    [TestMethod]
    public void GetHashCode_ShouldReturnStringHash()
    {
        int hash = DEFAULT_NAME_VALUE.GetHashCode();
        var name = new NoteName(DEFAULT_NAME_VALUE);
        
        Assert.AreEqual(hash, name.GetHashCode());
    }
}