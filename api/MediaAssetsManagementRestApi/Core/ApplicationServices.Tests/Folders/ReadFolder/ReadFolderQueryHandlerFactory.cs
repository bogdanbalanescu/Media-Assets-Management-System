using ApplicationServices.Requests.Queries.Folders.ReadFolder;
using Domain.Aggregates.Folders;
using Domain.SharedKernel.Exceptions;
using Moq;
using System.Threading.Tasks;

namespace ApplicationServices.Tests.Folders.ReadFolder
{
    internal static class ReadFolderQueryHandlerFactory
    {
        internal static ReadFolderQueryHandler Create(Folder folder)
        {
            var foldersRepository = new Mock<IFoldersRepository>();
            foldersRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(folder));

            return new ReadFolderQueryHandler(foldersRepository.Object);
        }

        internal static ReadFolderQueryHandler CreateForNotFoundRepositoryException()
        {
            var foldersRepository = new Mock<IFoldersRepository>();
            foldersRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .Throws<NotFoundRepositoryException>();

            return new ReadFolderQueryHandler(foldersRepository.Object);
        }
    }
}
