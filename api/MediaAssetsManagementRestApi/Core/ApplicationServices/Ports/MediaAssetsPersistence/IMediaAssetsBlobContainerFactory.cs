using Azure.Storage.Blobs;

namespace ApplicationServices.Ports.MediaAssetsPersistence
{
    public interface IMediaAssetsBlobContainerFactory
    {
        BlobContainerClient GetContainerClient();
    }
}
