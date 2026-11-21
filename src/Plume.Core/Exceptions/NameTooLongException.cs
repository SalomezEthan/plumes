namespace Plume.Core.Exceptions;

public class NameTooLongException(int maxLength) : Exception($"Le nom ne peut excéder {maxLength} caractères.")
{
}