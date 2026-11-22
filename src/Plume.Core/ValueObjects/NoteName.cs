using Plume.Core.Exceptions;

namespace Plume.Core.ValueObjects;

public class NoteName
{
    public const int MAX_NAME_LENGTH = 24;

    private string _name;

    public NoteName(string name)
    {
        name = name.Trim();

        if (name.Length == 0)
        {
            throw new ArgumentException("Le nom d'une note ne peut pas être vide.");
        }

        if (name.Length > MAX_NAME_LENGTH)
        {
            throw new NameTooLongException(MAX_NAME_LENGTH);
        }

        _name = name;
    }

    public override string ToString()
    {
        return _name;
    }

    public override bool Equals(object? obj)
    {
        return obj is NoteName other && _name == other._name;
    }

    public override int GetHashCode()
    {
        return _name.GetHashCode();
    }
}