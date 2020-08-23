using Domain.Aggregates.ImageAssets;
using System;

namespace ApplicationServices.Requests.Queries.ImageAssets.ReadImageAsset
{
    public class ImageAssetResponse
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public string Name { get; set; }
        public string ContentType { get; set; }
        public Guid Guid { get; set; }
        public int FolderId { get; set; }

        public ImageAssetResponse(ImageAsset imageAsset)
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