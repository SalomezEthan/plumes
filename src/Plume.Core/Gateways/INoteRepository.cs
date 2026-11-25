public interface INoteRepository
{
    public Task DeleteAsync(Guid noteId);
}