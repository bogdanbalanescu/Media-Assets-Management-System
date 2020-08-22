using ApplicationServices.Requests.Exceptions;
using Domain.Aggregates.ImageAssets;
using Domain.SharedKernel.Exceptions;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationServices.Requests.Queries.ImageAssets.ReadImageAsset
{
    public class ReadImageAssetQueryHandler : IRequestHandler<ReadImageAssetQuery, ImageAssetResponse>
    {
        private readonly IImageAssetsRepository imageAssetsRepository;

        public ReadImageAssetQueryHandler(IImageAssetsRepository imageAssetsRepository)
        {
            this.imageAssetsRepository = imageAssetsRepository;
        }

        public async Task<ImageAssetResponse> Handle(ReadImageAssetQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var imageAsset = await imageAssetsRepository.GetByIdAsync(request.Id);

                return new ImageAssetResponse(imageAsset);
            }
            catch (NotFoundRepositoryException)
            {
                throw new NotFoundRequestException();
            }
        }
    }
}
