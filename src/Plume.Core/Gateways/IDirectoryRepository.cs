using Plume.Core.Entities;

namespace Plume.Core.Gateways;

public interface IDirectoryRepository
{
    Task<Entities.Directory?> GetByIdAsync(Guid directoryId);
    Task<Entities.Directory?> GetNoteParentAsync(Guid noteId);
    Task SaveAsync(Entities.Directory dir);
}