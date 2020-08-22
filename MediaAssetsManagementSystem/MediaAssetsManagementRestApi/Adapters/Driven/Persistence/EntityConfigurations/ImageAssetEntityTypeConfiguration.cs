using Domain.Aggregates.Folders;
using Domain.Aggregates.ImageAssets;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations
{
    public class ImageAssetEntityTypeConfiguration : IEntityTypeConfiguration<ImageAsset>
    {
        public void Configure(EntityTypeBuilder<ImageAsset> builder)
        {
            builder.ToTable("ImageAssets");

            builder.HasKey(image => image.Id).IsClustered(false);

            builder.Property(image => image.Id).HasColumnName("Id").UseIdentityColumn().IsRequired();
            builder.Property(image => image.CreationDate).HasColumnName("CreationDate").HasDefaultValueSql("getdate()");
            builder.Property(image => image.Name).HasColumnName("Name").HasMaxLength(100).IsRequired();
            builder.Property(image => image.Guid).HasColumnName("Guid").HasMaxLength(100).IsRequired();

            builder.HasOne(typeof(Folder)).WithMany().HasForeignKey(nameof(ImageAsset.FolderId));
        }
    }
}
