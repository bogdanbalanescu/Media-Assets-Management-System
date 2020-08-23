using ApplicationServices.Ports.MediaAssetsPersistence;
using ApplicationServices.Requests.Exceptions;
using Azure.Storage.Blobs;
using Domain.Aggregates.ImageAssets;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationServices.Requests.Commands.ImageAssets.CreateImageAsset
{
    internal class CreateImageAssetCommandHandler : IRequestHandler<CreateImageAssetCommand, int>
    {
        private readonly IImageAssetsRepository imageAssetsRepository;
        private readonly BlobContainerClient blobContainerClient;

        public CreateImageAssetCommandHandler(
            IImageAssetsRepository imageAssetsRepository,
            IMediaAssetsBlobContainerFactory mediaAssetsBlobContainerFactory)
        {
            this.imageAssetsRepository = imageAssetsRepository;
            blobContainerClient = mediaAssetsBlobContainerFactory.GetContainerClient();
        }

        public async Task<int> Handle(CreateImageAssetCommand request, CancellationToken cancellationToken)
        {
            if (!await imageAssetsRepository.IsUniqueInFolder(request.FileName, request.ParentFolderId))
                throw new FileNameMustBeUniqueInFolderRequestException();

            var assetGlobalUniqueIdentifier = Guid.NewGuid();

            var blobClient = blobContainerClient.GetBlobClient(assetGlobalUniqueIdentifier.ToString());
            await blobClient.UploadAsync(request.ImageReadStream, true);
            request.ImageReadStream.Close();

            var imageAsset = new ImageAsset(request.FileName, request.ContentType, assetGlobalUniqueIdentifier, request.ParentFolderId);
            var imageId = await imageAssetsRepository.AddAsync(imageAsset);

            return imageId;
        }
    }
}
