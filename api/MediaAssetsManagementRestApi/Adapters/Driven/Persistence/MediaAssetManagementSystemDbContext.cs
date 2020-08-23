using Domain.Aggregates.Folders;
using Domain.Aggregates.ImageAssets;
using Microsoft.EntityFrameworkCore;
using Persistence.EntityConfigurations;

namespace Persistence
{
    public class MediaAssetManagementSystemDbContext : DbContext
    {
        public DbSet<Folder> Folders { get; set; }
        public DbSet<ImageAsset> ImageAssets { get; set; }

        public MediaAssetManagementSystemDbContext(DbContextOptions options) 
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new FolderEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ImageAssetEntityTypeConfiguration());
        }
    }
}
