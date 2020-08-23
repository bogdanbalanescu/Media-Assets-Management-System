using ApplicationServices.Ports.Persistence.DTOs.Folders;
using ApplicationServices.Requests.Exceptions;
using ApplicationServices.Requests.Pagination;
using ApplicationServices.Requests.Queries.Folders.ReadFolders;
using Domain.Aggregates.Folders;
using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationServices.Tests.Folders.ReadFolders
{
    [TestFixture]
    public class ReadFoldersQueryHandlerTests
    {
        [Test]
        public async Task ReadFolders_ThreeFoldersExist_ThreeFoldersAreRetrieved()
        {
            var foldersPagedSeed = GetFoldersSeed();

            var query = new ReadFoldersQuery();
            var queryHandler = ReadFoldersQueryHandlerFactory.Create(foldersPagedSeed);

            var response = await queryHandler.Handle(query, CancellationToken.None);

            response.Items.Count.ShouldBe(foldersPagedSeed.Items.Count);
            response.NextPageToken.ShouldBe(foldersPagedSeed.NextPageToken);

            foreach (var item in response.Items)
            {
                foldersPagedSeed.Items.ShouldContain(
                    x => x.Id == item.Id &&
                    x.CreationDate == item.CreationDate &&
                    x.Name == item.Name &&
                    x.ParentId == item.ParentId);
            }
        }

        [Test]
        public async Task ReadFolders_NoFoldersExist_EmptyPageIsReturned()
        {
            var foldersPagedSeed = new PaginatedResult<FolderDto>(new List<FolderDto>(), null);

            var query = new ReadFoldersQuery();
            var queryHandler = ReadFoldersQueryHandlerFactory.Create(foldersPagedSeed);

            var response = await queryHandler.Handle(query, CancellationToken.None);

            response.Items.Count.ShouldBe(0);
            response.NextPageToken.ShouldBeNull();
        }

        [Test]
        public void ReadFolders_NextPageTokenIsInvalid_ThrowsInvalidNextPageTokenRequestException()
        {
            var query = new ReadFoldersQuery(100, Guid.NewGuid().ToString());
            var queryHandler = ReadFoldersQueryHandlerFactory.CreateForInvalidNextPageTokenRepositoryException();

            Should.Throw<InvalidNextPageTokenRequestException>(
                async () => await queryHandler.Handle(query, CancellationToken.None));
        }

        private PaginatedResult<FolderDto> GetFoldersSeed()
        {
            var folderDtos = new List<FolderDto>
            {
                new FolderDto(new Folder(1, DateTime.UtcNow, "one")),
                new FolderDto(new Folder(2, DateTime.UtcNow, "two", 1)),
                new FolderDto(new Folder(3, DateTime.UtcNow, "three", 1))
            };
            return new PaginatedResult<FolderDto>(folderDtos, Guid.NewGuid().ToString());
        }
    }
}
