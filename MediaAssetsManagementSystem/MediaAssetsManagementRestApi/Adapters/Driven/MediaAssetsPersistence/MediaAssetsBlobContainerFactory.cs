using ApplicationServices.Ports.MediaAssetsPersistence;
using Azure.Storage;
using Azure.Storage.Blobs;
using Microsoft.Extensions.Options;
using System;

namespace MediaAssetsPersistence
{
    public class MediaAssetsBlobContainerFactory : IMediaAssetsBlobContainerFactory
    {
        private readonly Lazy<BlobContainerClient> lazyContainerClient;

        public MediaAssetsBlobContainerFactory(IOptions<MediaAssetsPersistenceConfigurationKeys> persistenceKeys)
        {
            lazyContainerClient = new Lazy<BlobContainerClient>(() =>
            {
                var blobServiceClient = new BlobServiceClient(
                    new Uri(persistenceKeys.Value.ConnectionString),
                    new StorageSharedKeyCredential(
                        persistenceKeys.Value.AccountName, 
                        persistenceKeys.Value.AccountKey)
                    );


                var containerClient = blobServiceClient.GetBlobContainerClient(persistenceKeys.Value.MediaAssetsContainerName);
                if (!containerClient.Exists())
                    containerClient = blobServiceClient.CreateBlobContainer(persistenceKeys.Value.MediaAssetsContainerName);

                return containerClient;
            });
        }

        public BlobContainerClient GetContainerClient() => lazyContainerClient.Value;
    }
}
