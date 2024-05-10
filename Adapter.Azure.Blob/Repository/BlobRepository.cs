using Adapter.Azure.Blob.Configurations;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Threading.Tasks;
namespace Hexagonal.Adapter.Blob.Repository
{
    internal class BlobRepository : IBlobRepository
    {
        private readonly BlobServiceClient _blobServiceClient;
        public BlobRepository(IOptions<BlobSettings> options)
        {
            if (string.IsNullOrEmpty(options.Value.ConnectionString))
            {
                throw new ArgumentException("BlobSetting ConnectionString not configurated");
            }

            _blobServiceClient = new BlobServiceClient(options.Value.ConnectionString);
        }

        public async Task<bool> ExistsAsync(string containerName, string blobName)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            var blobClient = containerClient.GetBlobClient(blobName);
            return await blobClient.ExistsAsync();
        }

        public async Task CreateAsync(string containerName, string blobName, Stream content, bool overwrite = false)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            var blobClient = containerClient.GetBlobClient(blobName);

            if (!overwrite && await blobClient.ExistsAsync())
            {
                throw new InvalidOperationException("Blob already exists.");
            }

            // Set the content type
            var headers = new BlobHttpHeaders { ContentType = FileHelper.GetContentTypeByFileName(blobName) };

            await blobClient.UploadAsync(content, new BlobUploadOptions { HttpHeaders = headers });
        }

        public async Task UpdateAsync(string containerName, string blobName, Stream content)
        {
            await CreateAsync(containerName, blobName, content, true);
        }

        public async Task DeleteAsync(string containerName, string blobName)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            var blobClient = containerClient.GetBlobClient(blobName);

            if (!await blobClient.ExistsAsync())
            {
                return;
            }

            await blobClient.DeleteAsync();
        }
    }
}