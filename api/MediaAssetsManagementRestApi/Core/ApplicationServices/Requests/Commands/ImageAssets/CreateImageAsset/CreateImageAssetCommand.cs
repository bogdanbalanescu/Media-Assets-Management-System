using ApplicationServices.Requests.Exceptions;
using EnsureThat;
using MediatR;
using System.IO;

namespace ApplicationServices.Requests.Commands.ImageAssets.CreateImageAsset
{
    /// <summary>
    /// Create a new image asset.
    /// </summary>
    /// <exception cref="FileNameMustBeUniqueInFolderRequestException">Thrown if a file with the same name already exists under the same folder.</exception>
    /// <exception cref="ParentFolderDoesNotExistRequestException">Thrown if parent folder does not exist.</exception>
    public class CreateImageAssetCommand : IRequest<int>
    {
        public Stream ImageReadStream { get; }
        public string FileName { get; }
        public string ContentType { get; set; }
        public int ParentFolderId { get; }

        public CreateImageAssetCommand(Stream imageReadStream, string fileName, string contentType, int parentFolderId)
        {
            EnsureArg.IsNotNull(imageReadStream, nameof(imageReadStream));
            ImageReadStream = imageReadStream;

            EnsureArg.IsNotNullOrEmpty(fileName, nameof(fileName));
            FileName = fileName;

            EnsureArg.IsNotNullOrEmpty(contentType, nameof(contentType));
            ContentType = contentType;

            ParentFolderId = parentFolderId;
        }
    }
}
