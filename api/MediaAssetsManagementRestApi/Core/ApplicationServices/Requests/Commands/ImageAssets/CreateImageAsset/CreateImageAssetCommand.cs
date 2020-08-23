using EnsureThat;
using MediatR;
using System.IO;

namespace ApplicationServices.Requests.Commands.ImageAssets.CreateImageAsset
{
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
