
namespace Hexagonal.Adapter.Blob
{
    public interface IBlobRepository
    {
        Task CreateAsync(string containerName, string blobName, Stream content, bool overwrite = false);
        Task DeleteAsync(string containerName, string blobName);
        Task<bool> ExistsAsync(string containerName, string blobName);
        Task UpdateAsync(string containerName, string blobName, Stream content);
    }
}