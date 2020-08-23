using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Options;
using Nito.AsyncEx;
using System;

namespace Persistence
{
    public class MediaAssetManagementSystemDbContextFactory
    {
        private const string ApplicationName = "MediaAssetManagementSystem.API";
        private readonly Lazy<MediaAssetManagementSystemDbContext> lazyDbContext;

        public MediaAssetManagementSystemDbContextFactory(IOptions<PersistenceConfigurationKeys> persistenceKeys)
        {
            lazyDbContext = new Lazy<MediaAssetManagementSystemDbContext>(() =>
            {
                var optionsBuilder = new DbContextOptionsBuilder<MediaAssetManagementSystemDbContext>();

                AsyncContext.Run(() =>
                {
                    string applicationConnectionString = $"Application Name={ApplicationName};{persistenceKeys.Value.ConnectionString}";

                    optionsBuilder.UseSqlServer(applicationConnectionString, options =>
                    {
                        options.EnableRetryOnFailure();
                    });
                });

                return new MediaAssetManagementSystemDbContext(optionsBuilder.Options);
            });
        }

        public MediaAssetManagementSystemDbContext GetDbContext() => lazyDbContext.Value;
    }

    public class MediaAssetManagementSystemDesignDbContextFactory : IDesignTimeDbContextFactory<MediaAssetManagementSystemDbContext>
    {
        private const string ApplicationName = "MediaAssetManagementSystem.API";

        public MediaAssetManagementSystemDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MediaAssetManagementSystemDbContext>();
            string connectionString = "Server=localhost; Database=MediaAssetManagementSystem.DB; Integrated Security=true";
            string applicationConnectionString = $"Application Name={ApplicationName};{connectionString}";

            optionsBuilder.UseSqlServer(applicationConnectionString, options =>
            {
                options.EnableRetryOnFailure();
            });

            return new MediaAssetManagementSystemDbContext(optionsBuilder.Options);
        }
    }
}
