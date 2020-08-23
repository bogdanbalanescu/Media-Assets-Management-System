using EnsureThat;
using MediatR;
using System.IO;

namespace ApplicationServices.Requests.Commands.ImageAssets.CreateImageAsset
{
    public class CreateImageAssetCommand : IRequest<int>
    {
        public Stream ImageReadStream { get; }
        public string FileName { get; }
        public int ParentFolderId { get; }

        public CreateImageAssetCommand(Stream imageReadStream, string fileName, int parentFolderId)
        {
            EnsureArg.IsNotNull(imageReadStream, nameof(imageReadStream));
            ImageReadStream = imageReadStream;

            EnsureArg.IsNotNullOrEmpty(fileName, nameof(fileName));
            FileName = fileName;

            ParentFolderId = parentFolderId;
        }
    }
}
