using ApplicationServices.Requests.Commands.Folders.CreateFolder;
using Domain.Aggregates.Folders;
using Moq;
using System.Threading.Tasks;

namespace ApplicationServices.Tests.Folders.CreateFolder
{
    internal static class CreateFoldersCommandHandlerFactory
    {
        internal static CreateFoldersCommandHandlerSetup CreateSetup(
            bool exists = true,
            bool isUniqueInParent = true)
        {
            var foldersRepository = new Mock<IFoldersRepository>();
            foldersRepository.Setup(x => x.Exists(It.IsAny<int>()))
                .Returns(Task.FromResult(exists));
            foldersRepository.Setup(x => x.IsUniqueInParent(It.IsAny<string>(), It.IsAny<int?>()))
                .Returns(Task.FromResult(isUniqueInParent));

            return new CreateFoldersCommandHandlerSetup
            {
                Handler = new CreateFolderCommandHandler(foldersRepository.Object),
                FoldersRepository = foldersRepository
            };
        }
    }

    public class CreateFoldersCommandHandlerSetup
    {
        internal CreateFolderCommandHandler Handler { get; set; }
        public Mock<IFoldersRepository> FoldersRepository { get; internal set; }
    }
}
