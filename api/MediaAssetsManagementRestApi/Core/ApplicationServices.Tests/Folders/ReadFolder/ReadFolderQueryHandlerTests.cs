using ApplicationServices.Requests.Exceptions;
using ApplicationServices.Requests.Queries.Folders.ReadFolder;
using Domain.Aggregates.Folders;
using NUnit.Framework;
using Shouldly;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationServices.Tests.Folders.ReadFolder
{
    [TestFixture]
    public class ReadFolderQueryHandlerTests
    {
        [Test]
        public async Task ReadFolder_FolderExists_FolderIsRetrieved()
        {
            var folderSeed = new Folder(1, DateTime.UtcNow, "folderName", 2);
            var query = new ReadFolderQuery(folderSeed.Id);
            var queryHandler = ReadFolderQueryHandlerFactory.Create(folderSeed);

            var response = await queryHandler.Handle(query, CancellationToken.None);

            response.ShouldSatisfyAllConditions(
                () => response.Id.ShouldBe(folderSeed.Id),
                () => response.CreationDate.ShouldBe(folderSeed.CreationDate),
                () => response.Name.ShouldBe(folderSeed.Name),
                () => response.ParentId.ShouldBe(folderSeed.ParentId)
                );
        }

        [Test]
        public void ReadFolder_FolderDoesNotExist_ThrowsNotFoundRequestException()
        {
            var query = new ReadFolderQuery(1);
            var queryHandler = ReadFolderQueryHandlerFactory.CreateForNotFoundRepositoryException();

            Should.Throw<NotFoundRequestException>(
                async () => await queryHandler.Handle(query, CancellationToken.None));
        }
    }
}
