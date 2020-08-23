using ApplicationServices.Ports.Persistence;
using ApplicationServices.Ports.Persistence.DTOs.Folders;
using ApplicationServices.Ports.Persistence.Exceptions;
using ApplicationServices.Requests.Pagination;
using ApplicationServices.Requests.Queries.Folders.ReadFolders;
using Moq;
using System.Threading.Tasks;

namespace ApplicationServices.Tests.Folders.ReadFolders
{
    internal static class ReadFoldersQueryHandlerFactory
    {
        internal static ReadFoldersQueryHandler Create(PaginatedResult<FolderDto> foldersPagedSeed)
        {
            var foldersQueriesRepository = new Mock<IFoldersQueriesRepository>();
            foldersQueriesRepository.Setup(x => x.GetPageAsync(It.IsAny<PaginationDto>(), It.IsAny<int?>()))
                .Returns(Task.FromResult(foldersPagedSeed));

            return new ReadFoldersQueryHandler(foldersQueriesRepository.Object);
        }

        internal static ReadFoldersQueryHandler CreateForInvalidNextPageTokenRepositoryException()
        {
            var foldersQueriesRepository = new Mock<IFoldersQueriesRepository>();
            foldersQueriesRepository.Setup(x => x.GetPageAsync(It.IsAny<PaginationDto>(), It.IsAny<int?>()))
                .Throws<InvalidNextPageTokenRepositoryException>(); 

            return new ReadFoldersQueryHandler(foldersQueriesRepository.Object);
        }
    }
}
