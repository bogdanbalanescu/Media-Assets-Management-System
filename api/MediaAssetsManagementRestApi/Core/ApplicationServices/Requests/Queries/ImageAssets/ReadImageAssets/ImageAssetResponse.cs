using ApplicationServices.Ports.Persistence.DTOs.ImageAssets;
using System;

namespace ApplicationServices.Requests.Queries.ImageAssets.ReadImageAssets
{
    public class ImageAssetResponse
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public string Name { get; set; }
        public string ContentType { get; set; }
        public Guid Guid { get; set; }
        public int FolderId { get; set; }

        public ImageAssetResponse(ImageAssetDto imageAsset)
        {
            Id = imageAsset.Id;
            CreationDate = imageAsset.CreationDate;
            Name = imageAsset.Name;
            ContentType = imageAsset.ContentType;
            Guid = imageAsset.Guid;
            FolderId = imageAsset.FolderId;
        }
    }
}