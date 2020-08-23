using ApplicationServices.Requests.Exceptions;
using ApplicationServices.Requests.Pagination;
using MediatR;

namespace ApplicationServices.Requests.Queries.Folders.ReadFolders
{
    /// <summary>
    /// Retrieves a page of Folders.
    /// </summary>
    /// <exception cref="InvalidNextPageTokenRequestException">Thrown if next page token is invalid.</exception>
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
