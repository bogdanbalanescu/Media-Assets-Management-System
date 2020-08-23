using Domain.SharedKernel;
using Domain.SharedKernel.Exceptions;
using System.Threading.Tasks;

namespace Domain.Aggregates.Folders
{
    public interface IFoldersRepository : IRepository<Folder, int>
    {
        /// <summary>
        /// Retrieve one folder by its identifier.
        /// </summary>
        /// <param name="id">Folder Identifier. Required parameter.</param>
        /// <returns>Folder</returns>
        /// <exception cref="RepositoryException">Thrown for various repository exceptions.</exception>
        /// <exception cref="NotFoundRepositoryException">Thrown if the folder referenced by the provided identifier was not found.</exception>
        Task<Folder> GetByIdAsync(int id);

        /// <summary>
        /// Check if folder referenced by its identifier exists.
        /// </summary>
        /// <param name="id">Folder Identifier. Required parameter.</param>
        /// <returns>True if folder exists, false otherwise.</returns>
        Task<bool> Exists(int id);

        /// <summary>
        /// Check if folder name is unique in parent folder.
        /// </summary>
        /// <param name="name">Folder name. Required parameter.</param>
        /// <param name="parentId">Folder parent identifier. required parameter.</param>
        /// <returns></returns>
        Task<bool> IsUniqueInParent(string name, int? parentId);

        /// <summary>
        /// Add a new folder.
        /// </summary>
        /// <param name="folder">Folder to add. Required parameter.</param>
        /// <returns>Identifier of the newly added folder.</returns>
        /// <exception cref="RepositoryException">Thrown for various repository exceptions.</exception>
        Task<int> AddAsync(Folder folder);
    }
}
