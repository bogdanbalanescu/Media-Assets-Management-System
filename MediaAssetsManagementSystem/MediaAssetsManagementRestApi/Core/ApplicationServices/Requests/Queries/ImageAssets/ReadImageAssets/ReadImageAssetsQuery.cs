using ApplicationServices.Requests.Pagination;
using MediatR;

namespace ApplicationServices.Requests.Queries.ImageAssets.ReadImageAssets
{
    public class ReadImageAssetsQuery : IRequest<PaginatedResult<ImageAssetResponse>>
    {
        public int Limit;
        public string NextPageToken { get; set; }
        public int FolderId { get; set; }

        public ReadImageAssetsQuery(int parentId, int limit = 100, string nextPageToken = null)
        {
            Limit = limit;
            NextPageToken = nextPageToken;
            FolderId = parentId;
        }
    }
}
