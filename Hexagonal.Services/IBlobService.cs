namespace Hexagonal.Services
{
    public interface IBlobService
    {
        Task<bool> Exists(string containerName, string blobName);
        Task<string> Create(string containerName, string extension, Stream content, bool overwrite = false);
        Task Delete(string containerName, string blobName);
        Task<string> Update(string containerName, string blobName, string extension, Stream content);
    }
}