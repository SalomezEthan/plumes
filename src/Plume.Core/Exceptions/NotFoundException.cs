namespace Plume.Core.Exceptions;

public class NotFoundException(string resourceName, Guid id) : Exception($"{resourceName} n°{id} est introuvable.")
{
}