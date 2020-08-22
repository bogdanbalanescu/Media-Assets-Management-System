using ApplicationServices.Ports.Persistence;
using ApplicationServices.Ports.Persistence.DTOs.ImageAssets;
using ApplicationServices.Requests.Pagination;
using Domain.Aggregates.ImageAssets;
using Domain.SharedKernel.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Persistence.Repositories.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class ImageAssetsRepository : IImageAssetsRepository, IImageAssetsQueriesRepository
    {
        private readonly MediaAssetManagementSystemDbContext dbContext;
        private readonly ILogger<ImageAssetsRepository> logger;

        public ImageAssetsRepository(MediaAssetManagementSystemDbContextFactory dbContextFactory, ILogger<ImageAssetsRepository> logger)
        {
            this.dbContext = dbContextFactory.GetDbContext();
            this.logger = logger;
        }

        public async Task<PaginatedResult<ImageAssetDto>> GetPageAsync(PaginationDto pagination, int folderId)
        {
            int pageIndex = !string.IsNullOrEmpty(pagination.NextPageToken) ?
                new NextPageToken(pagination.NextPageToken).NextPageIndex : 1;
            try
            {
                IQueryable<ImageAsset> imageAssetsQuery = dbContext.ImageAssets.AsNoTracking();

                var imageAssets = await imageAssetsQuery
                    .Where(imageAsset => imageAsset.FolderId == folderId)
                    .Skip((pageIndex - 1) * pagination.Limit)
                    .Take(pagination.Limit + 1)
                    .ToListAsync();

                bool hasNextPage = imageAssets.Count > pagination.Limit;
                if (hasNextPage)
                {
                    imageAssets.RemoveAt(imageAssets.Count - 1);
                }

                string nextPageToken = new NextPageToken(pageIndex, hasNextPage).ToString();

                return new PaginatedResult<ImageAssetDto>(
                    imageAssets.Select(imageAsset => new ImageAssetDto(imageAsset)).OrderBy(imageAsset => imageAsset.Id).ToList(),
                    nextPageToken
                    );
            }
            catch (Exception ex)
            {
                string errorMessage = $"Failed to retrieve page of {typeof(ImageAsset)}";
                logger.LogError(errorMessage, ex);

                throw new RepositoryException(errorMessage, ex);
            }
        }

        public async Task<ImageAsset> GetByIdAsync(int id)
        {
            try
            {
                IQueryable<ImageAsset> imageAssetsQuery = dbContext.ImageAssets.AsNoTracking();

                var imageAsset = await imageAssetsQuery
                    .Where(imageAsset => imageAsset.Id == id)
                    .FirstOrDefaultAsync();

                if (imageAsset == default)
                    throw new NotFoundRepositoryException();

                return imageAsset;
            }
            catch (Exception ex) when (!(ex is NotFoundRepositoryException))
            {
                string errorMessage = $"Failed to retrieve {typeof(ImageAsset)} with id: {id}";
                logger.LogError(errorMessage, ex);

                throw new RepositoryException(errorMessage, ex);
            }
        }

        public async Task<bool> IsUniqueInFolder(string name, int? folderId)
        {
            IQueryable<ImageAsset> imageAssetsQuery = dbContext.ImageAssets.AsNoTracking();

            var doesImageAssetAlreadyExist = await imageAssetsQuery
                .Where(imageAsset => imageAsset.FolderId == folderId && imageAsset.Name == name)
                .AnyAsync();

            return !doesImageAssetAlreadyExist;
        }

        public async Task<int> AddAsync(ImageAsset asset)
        {
            try
            {
                var addedImageAsset = dbContext.ImageAssets.Add(asset);
                await dbContext.SaveChangesAsync();

                return addedImageAsset.Entity.Id;
            }
            catch (Exception ex)
            {
                string errorMessage = $"Failed to add {typeof(ImageAsset)}";
                logger.LogError(errorMessage, ex);

                throw new RepositoryException(errorMessage, ex);
            }
        }
    }
}
