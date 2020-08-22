using Api.Models.Resources;
using System;

namespace Api.Models.ImageAssets
{
    public class ImageAssetResource : Resource
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public string Name { get; set; }
        public Guid Guid { get; set; }
        public int? FolderId { get; set; }

        public ImageAssetResource() { }

        public ImageAssetResource(ApplicationServices.Requests.Queries.ImageAssets.ReadImageAsset.ImageAssetResponse imageAsset)
        {
            Id = imageAsset.Id;
            CreationDate = imageAsset.CreationDate;
            Name = imageAsset.Name;
            Guid = imageAsset.Guid;
            FolderId = imageAsset.FolderId;
        }

        public ImageAssetResource(ApplicationServices.Requests.Queries.ImageAssets.ReadImageAssets.ImageAssetResponse imageAsset)
        {
            Id = imageAsset.Id;
            CreationDate = imageAsset.CreationDate;
            Name = imageAsset.Name;
            Guid = imageAsset.Guid;
            FolderId = imageAsset.FolderId;
        }
    }
}
