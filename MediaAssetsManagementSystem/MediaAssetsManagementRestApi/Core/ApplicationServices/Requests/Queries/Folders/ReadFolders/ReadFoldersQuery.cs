using ApplicationServices.Requests.Pagination;
using MediatR;

namespace ApplicationServices.Requests.Queries.Folders.ReadFolders
{
    public class ReadFoldersQuery : IRequest<PaginatedResult<FolderResponse>>
    {
        public int Limit;
        public string NextPageToken { get; set; }
        public int? ParentId { get; set; }

        public ReadFoldersQuery(int limit = 100, string nextPageToken = null, int? parentId = null)
        {
            Limit = limit;
            NextPageToken = nextPageToken;
            ParentId = parentId;
        }
    }
}
