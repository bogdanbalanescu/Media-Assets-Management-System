using Azure.Storage.Blobs;

namespace ApplicationServices.Ports.MediaAssetsPersistence
{
    public interface IMediaAssetsBlobContainerFactory
    {
        /// <summary>
        /// Creates or retrieves an existing BlobContainerClient
        /// </summary>
        /// <returns>BlobContainerClient</returns>
        BlobContainerClient GetContainerClient();
    }
}
