using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System.Text;

namespace Cognizant.Training.StorageAccount.API.Repository
{
    public class BlobRepository
    {
        private readonly BlobContainerClient blobContainerClient;
        
        public BlobRepository(BlobContainerClient blobContainerClient)
        {
            this.blobContainerClient = blobContainerClient;
        }

        public async Task<string> AddBlob(string blobName)
        {
            var blobClient = blobContainerClient.GetBlobClient(blobName);
            var blobContent = new BinaryData(Encoding.UTF8.GetBytes("This is a sample Blob Content"));            
            await blobClient.UploadAsync(blobContent, overwrite: true);
            var metadata = new Dictionary<string, string>();
            metadata.Add("docType", "textDocuments");
            metadata.Add("purpose", "training");
            metadata["category"] = "guidance";
            // Set the blob's metadata.
            await blobClient.SetMetadataAsync(metadata);
            return blobClient.Uri.ToString();
        }

        public async Task<List<string>> GetBlobs()
        {
            var blobs = new List<string>();
            await foreach (BlobItem blobItem in blobContainerClient.GetBlobsAsync())
            {                
                blobs.Add(blobItem.Name);
            }
            return blobs;
        }
    }
}
