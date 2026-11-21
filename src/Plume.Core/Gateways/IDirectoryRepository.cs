using Plume.Core.Entities;

namespace Plume.Core.Gateways;

public interface IDirectoryRepository
{
    Task<Entities.Directory?> GetById(Guid directoryId);
    Task SaveAsync(Entities.Directory dir);
}