using ApplicationServices.Ports.Persistence.DTOs.ImageAssets;
using ApplicationServices.Requests.Pagination;
using Domain.SharedKernel.Exceptions;
using System.Threading.Tasks;

namespace ApplicationServices.Ports.Persistence
{
    public interface IImageAssetsQueriesRepository
    {
        /// <summary>
        /// Retrieves a page of Image Assets.
        /// </summary>
        /// <param name="pagination">Pagination parameters. Required parameter.</param>
        /// <param name="folderId">Folder Parent Identifier. Required parameter. When present, image assets are filtered by their parent identifier.</param>
        /// <returns>A page of Image Assets.</returns>
        /// <exception cref="RepositoryException">Thrown for various repository exceptions.</exception>
        /// <exception cref="InvalidNextPageTokenRepositoryException">Thrown if the continuation token is invalid.</exception>
        Task<PaginatedResult<ImageAssetDto>> GetPageAsync(PaginationDto pagination, int folderId);
    }
}
