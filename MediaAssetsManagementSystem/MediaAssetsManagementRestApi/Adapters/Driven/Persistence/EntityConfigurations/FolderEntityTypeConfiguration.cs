using Domain.Aggregates.Folders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations
{
    public class FolderEntityTypeConfiguration : IEntityTypeConfiguration<Folder>
    {
        public void Configure(EntityTypeBuilder<Folder> builder)
        {
            builder.ToTable("Folders");

            builder.HasKey(folder => folder.Id).IsClustered(false);

            builder.Property(folder => folder.Id).HasColumnName("Id").UseIdentityColumn().IsRequired();
            builder.Property(folder => folder.CreationDate).HasColumnName("CreationDate").HasDefaultValueSql("getdate()");
            builder.Property(folder => folder.Name).HasColumnName("Name").HasMaxLength(100).IsRequired();

            builder.HasOne(typeof(Folder)).WithMany().HasForeignKey(nameof(Folder.ParentId));
        }
    }
}
