using Domain.Aggregates.ImageAssets;
using System;

namespace ApplicationServices.Ports.Persistence.DTOs.ImageAssets
{
    public class ImageAssetDto
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public string Name { get; set; }
        public Guid Guid { get; set; }
        public int FolderId { get; set; }

        public ImageAssetDto(ImageAsset imageAsset)
        {
            Id = imageAsset.Id;
            CreationDate = imageAsset.CreationDate;
            Name = imageAsset.Name;
            Guid = imageAsset.Guid;
            FolderId = imageAsset.FolderId;
        }
    }
}
