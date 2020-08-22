using ApplicationServices.Ports.Persistence;
using ApplicationServices.Ports.Persistence.DTOs.Folders;
using ApplicationServices.Requests.Pagination;
using Domain.Aggregates.Folders;
using Domain.SharedKernel.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Persistence.Repositories.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class FoldersRepository : IFoldersRepository, IFoldersQueriesRepository
    {
        private readonly MediaAssetManagementSystemDbContext dbContext;
        private readonly ILogger<FoldersRepository> logger;

        public FoldersRepository(MediaAssetManagementSystemDbContextFactory dbContextFactory, ILogger<FoldersRepository> logger)
        {
            this.dbContext = dbContextFactory.GetDbContext();
            this.logger = logger;
        }

        public async Task<PaginatedResult<FolderDto>> GetPageAsync(PaginationDto pagination, int? parentId)
        {
            int pageIndex = !string.IsNullOrEmpty(pagination.NextPageToken) ?
                new NextPageToken(pagination.NextPageToken).NextPageIndex : 1;
            try
            {
                IQueryable<Folder> foldersQuery = dbContext.Folders.AsNoTracking();

                var folders = await foldersQuery
                    .Where(folder => folder.ParentId == parentId)
                    .Skip((pageIndex - 1) * pagination.Limit)
                    .Take(pagination.Limit + 1)
                    .ToListAsync();

                bool hasNextPage = folders.Count > pagination.Limit;
                if (hasNextPage)
                {
                    folders.RemoveAt(folders.Count - 1);
                }

                string nextPageToken = new NextPageToken(pageIndex, hasNextPage).ToString();

                return new PaginatedResult<FolderDto>(
                    folders.Select(folder => new FolderDto(folder)).OrderBy(folder => folder.Id).ToList(),
                    nextPageToken
                    );
            }
            catch (Exception ex)
            {
                string errorMessage = $"Failed to retrieve page of {typeof(Folder)}";
                logger.LogError(errorMessage, ex);

                throw new RepositoryException(errorMessage, ex);
            }
        }

        public async Task<Folder> GetByIdAsync(int id)
        {
            try
            {
                IQueryable<Folder> foldersQuery = dbContext.Folders.AsNoTracking();

                var folder = await foldersQuery
                    .Where(folder => folder.Id == id)
                    .FirstOrDefaultAsync();

                if (folder == default)
                    throw new NotFoundRepositoryException();

                return folder;
            }
            catch (Exception ex) when (!(ex is NotFoundRepositoryException))
            {
                string errorMessage = $"Failed to retrieve {typeof(Folder)} with id: {id}";
                logger.LogError(errorMessage, ex);

                throw new RepositoryException(errorMessage, ex);
            }
        }

        public async Task<bool> IsUniqueInParent(string name, int? parentId)
        {
            IQueryable<Folder> foldersQuery = dbContext.Folders.AsNoTracking();

            var doesFolderAlreadyExist = await foldersQuery
                .Where(folder => folder.ParentId == parentId && folder.Name == name)
                .AnyAsync();

            return !doesFolderAlreadyExist;
        }

        public async Task<int> AddAsync(Folder folder)
        {
            try
            {
                var addedFolder = dbContext.Folders.Add(folder);
                await dbContext.SaveChangesAsync();

                return addedFolder.Entity.Id;
            }
            catch (Exception ex)
            {
                string errorMessage = $"Failed to add {typeof(Folder)}";
                logger.LogError(errorMessage, ex);

                throw new RepositoryException(errorMessage, ex);
            }
        }

    }
}
