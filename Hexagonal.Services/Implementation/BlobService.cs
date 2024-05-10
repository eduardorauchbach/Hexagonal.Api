using Hexagonal.Adapter.Blob;
using Hexagonal.Common.Extensions;

namespace Hexagonal.Services.Implementation
{
    internal class BlobService : IBlobService
    {
        private readonly IBlobRepository _blobRepository;
        public BlobService(IBlobRepository blobRepository)
        {
            _blobRepository = blobRepository ?? throw new ArgumentNullException(nameof(blobRepository));
        }

        public async Task<bool> Exists(string containerName, string blobName)
        {
            return await _blobRepository.ExistsAsync(containerName, blobName);
        }

        public async Task<string> Create(string containerName, string extension, Stream content, bool overwrite = false)
        {
            var blobName = GenerateBlobName(extension);

            await _blobRepository.CreateAsync(containerName, blobName, content, overwrite);

            return blobName;
        }

        public async Task<string> Update(string containerName, string blobName, string extension, Stream content)
        {
            //In same cases the extension of the file can change and it required the exclusion
            if (extension is not null)
            {
                if (blobName.GetExtension() != extension)
                {
                    await Delete(containerName, blobName);
                    blobName = GenerateBlobName(extension);
                }
            }

            await _blobRepository.UpdateAsync(containerName, blobName, content);

            return blobName;
        }

        public async Task Delete(string containerName, string blobName)
        {
            await _blobRepository.DeleteAsync(containerName, blobName);
        }

        private static string GenerateBlobName(string extension)
        {
            return $"{Guid.NewGuid()}-{Guid.NewGuid()}.{extension}";
        }
    }
}
