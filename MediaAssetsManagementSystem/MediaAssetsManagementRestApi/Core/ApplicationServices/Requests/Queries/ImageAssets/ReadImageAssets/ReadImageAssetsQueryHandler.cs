using ApplicationServices.Ports.Persistence;
using ApplicationServices.Ports.Persistence.Exceptions;
using ApplicationServices.Requests.Exceptions;
using ApplicationServices.Requests.Pagination;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationServices.Requests.Queries.ImageAssets.ReadImageAssets
{
    internal class ReadImageAssetsQueryHandler : IRequestHandler<ReadImageAssetsQuery, PaginatedResult<ImageAssetResponse>>
    {
        IImageAssetsQueriesRepository imageAssetsQueriesRepository;

        public ReadImageAssetsQueryHandler(IImageAssetsQueriesRepository imageAssetsQueriesRepository)
        {
            this.imageAssetsQueriesRepository = imageAssetsQueriesRepository;
        }

        public async Task<PaginatedResult<ImageAssetResponse>> Handle(ReadImageAssetsQuery request, CancellationToken cancellationToken)
        {
            var paginationDto = new PaginationDto
            {
                Limit = request.Limit,
                NextPageToken = request.NextPageToken
            };

            try
            {
                var imageAssetsPage = await imageAssetsQueriesRepository.GetPageAsync(paginationDto, request.FolderId);
                var imageAssetsResponse = imageAssetsPage.Items.Select(imageAsset => new ImageAssetResponse(imageAsset)).ToList();

                return new PaginatedResult<ImageAssetResponse>(imageAssetsResponse, imageAssetsPage.NextPageToken);
            }
            catch (InvalidNextPageTokenRepositoryException)
            {
                throw new InvalidNextPageTokenRequestException();
            }
        }
    }
}
