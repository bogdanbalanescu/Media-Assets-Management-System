using Domain.SharedKernel.Exceptions;
using System.Threading.Tasks;

namespace Domain.Aggregates.ImageAssets
{
    public interface IImageAssetsRepository
    {
        /// <summary>
        /// Retrieve one asset by its identifier.
        /// </summary>
        /// <param name="id">Asset Identifier. Required parameter.</param>
        /// <returns>Aseet</returns>
        /// <exception cref="RepositoryException">Thrown for various repository exceptions.</exception>
        /// <exception cref="NotFoundRepositoryException">Thrown if the iamge asset referenced by the provided identifier was not found.</exception>
        Task<ImageAsset> GetByIdAsync(int id);

        /// <summary>
        /// Add a new asset.
        /// </summary>
        /// <param name="asset">Asset to add. Required parameter.</param>
        /// <returns>Identifier of the newly added aseet.</returns>
        /// <exception cref="RepositoryException">Thrown for various repository exceptions.</exception>
        Task<int> AddAsync(ImageAsset asset);

        /// <summary>
        /// Check if asset name is unique in parent folder.
        /// </summary>
        /// <param name="name">Asset name. Required parameter.</param>
        /// <param name="folderId">Folder parent identifier. Required parameter.</param>
        /// <returns></returns>
        Task<bool> IsUniqueInFolder(string name, int? folderId);
    }
}
