using ApplicationServices.Ports.Persistence.DTOs.Folders;
using ApplicationServices.Ports.Persistence.Exceptions;
using ApplicationServices.Requests.Pagination;
using System.Threading.Tasks;

namespace ApplicationServices.Ports.Persistence
{
    public interface IFoldersQueriesRepository
    {
        /// <summary>
        /// Retrieves a page of Folders.
        /// </summary>
        /// <param name="pagination">Pagination parameters. Required parameter.</param>
        /// <param name="parentId">Parent Identifier. Optional parameter. When present, folders are filtered by their parent identifier.</param>
        /// <returns>A page of Folders.</returns>
        /// <exception cref="RepositoryException">Thrown for various repository exceptions.</exception>
        /// <exception cref="InvalidNextPageTokenRepositoryException">Thrown if the continuation token is invalid.</exception>
        Task<PaginatedResult<FolderDto>> GetPageAsync(PaginationDto pagination, int? parentId);
    }
}
