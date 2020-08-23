using Domain.SharedKernel;
using Domain.SharedKernel.Exceptions;
using EnsureThat;
using System;

namespace Domain.Aggregates.ImageAssets
{
    /// <summary>
    /// Image Asset aggregate. Defines an image media asset. May contain multiple ImageAssetVariant(s).
    /// </summary>
    public class ImageAsset : Aggregate<int>
    {
        /// <summary>
        /// Asset Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Asset Content Type
        /// </summary>
        public string ContentType { get; set; }

        /// <summary>
        /// Asset Global Unique Identifier
        /// </summary>
        public Guid Guid { get; set; }

        /// <summary>
        /// Parent Folder Identifier
        /// </summary>
        public int FolderId { get; set; }

        /// <summary>
        /// Asset constructor.
        /// </summary>
        /// <param name="name">Asset Name. Required parameter.</param>
        /// <param name="assetVariants">Asset Variants (one for each Variant type). Required parameter.</param>
        /// <param name="guid">Asset Global Unique Identifier. Required parameter.</param>
        public ImageAsset(string name, string contentType, Guid guid, int folderId)
        {
            SetName(name);
            SetContentType(contentType);
            SetGuid(guid);
            FolderId = folderId;
        }

        /// <summary>
        /// ImageAsset constructor.
        /// </summary>
        /// <param name="id">ImageAsset Identifier. Required parameter.</param>
        /// <param name="creationDate">Creation Date. Required parameter.</param>
        /// <param name="name">ImageAsset Name. Required parameter.</param>
        /// <param name="folderId">Parent Folder Identifier. Optional parameter. When optional, this folder has no parent.</param>
        /// <exception cref="RequiredArgumentException">Thrown if required arguments are null or empty.</exception>
        public ImageAsset(int id, DateTime creationDate, string name, string contentType, Guid guid, int folderId)
            : base(id, creationDate)
        {
            SetName(name);
            SetContentType(contentType);
            SetGuid(guid);
            FolderId = folderId;
        }

        /// <summary>
        /// Set the asset name.
        /// </summary>
        /// <param name="name">Asset Name. Required parameter.</param>
        /// <exception cref="RequiredArgumentException">Thrown if required arguments are null or empty.</exception>
        private void SetName(string name)
        {
            EnsureArg.IsNotNullOrEmpty(name, nameof(name),
                o => o.WithException(new RequiredArgumentException(nameof(name))));
            Name = name;
        }

        /// <summary>
        /// Set the asset ContentType.
        /// </summary>
        /// <param name="contentType">Asset ContentType. Required parameter.</param>
        /// <exception cref="RequiredArgumentException">Thrown if required arguments are null or empty.</exception>
        private void SetContentType(string contentType)
        {
            EnsureArg.IsNotNullOrEmpty(contentType, nameof(contentType),
                o => o.WithException(new RequiredArgumentException(nameof(contentType))));
            ContentType = contentType;
        }

        private void SetGuid(Guid guid)
        {
            EnsureArg.IsNotDefault(guid, nameof(guid),
                o => o.WithException(new RequiredArgumentException(nameof(guid))));
            Guid = guid;
        }
    }
}
