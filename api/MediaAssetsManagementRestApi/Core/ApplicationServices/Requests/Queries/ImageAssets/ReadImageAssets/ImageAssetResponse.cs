using ApplicationServices.Ports.Persistence.DTOs.ImageAssets;
using System;

namespace ApplicationServices.Requests.Queries.ImageAssets.ReadImageAssets
{
    public class ImageAssetResponse
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Asset Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Asset Global Unique Identifier
        /// </summary>
        public Guid Guid { get; set; }

        /// <summary>
        /// Parent Folder Identifier
        /// </summary>
        public int FolderId { get; set; }

        public ImageAssetResponse(ImageAssetDto imageAsset)
        {
            Id = imageAsset.Id;
            CreationDate = imageAsset.CreationDate;
            Name = imageAsset.Name;
            Guid = imageAsset.Guid;
            FolderId = imageAsset.FolderId;
        }
    }
}