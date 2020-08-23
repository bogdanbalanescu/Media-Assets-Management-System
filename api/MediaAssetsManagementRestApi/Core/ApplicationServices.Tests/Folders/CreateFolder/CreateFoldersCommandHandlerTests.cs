using ApplicationServices.Requests.Commands.Folders.CreateFolder;
using ApplicationServices.Requests.Commands.Folders.CreateFolder.Exceptions;
using ApplicationServices.Requests.Exceptions;
using Domain.Aggregates.Folders;
using Moq;
using NUnit.Framework;
using Shouldly;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationServices.Tests.Folders.CreateFolder
{
    [TestFixture]
    public class CreateFoldersCommandHandlerTests
    {
        [Test]
        [TestCase(3)]
        [TestCase(null)]
        public async Task CreateFolder_ValidAndUniqueName_FolderIsCreatedWithCorrectValues(int? parentId)
        {
            var command = new CreateFolderCommand("newFolder", parentId);
            var handlerSetup = CreateFoldersCommandHandlerFactory.CreateSetup(
                exists: true, isUniqueInParent: true);
            var handler = handlerSetup.Handler;

            await handler.Handle(command, CancellationToken.None);

            handlerSetup.FoldersRepository.Verify(
                x => x.AddAsync(It.IsAny<Folder>()), Times.Once);
        }

        [Test]
        public void CreateFolder_FolderAlreadyExists_ThrowsFolderNameMustBeUniqueInParentRequestException()
        {
            var command = new CreateFolderCommand("newFolder", 3);
            var handlerSetup = CreateFoldersCommandHandlerFactory.CreateSetup(
                isUniqueInParent: false);
            var handler = handlerSetup.Handler;

            Should.Throw<FolderNameMustBeUniqueInParentRequestException>(
                async () => await handler.Handle(command, CancellationToken.None));
        }

        [Test]
        public void CreateFolder_ParentFolderDoesNotExists_ThrowsParentFolderDoesNotExistRequestException()
        {
            var command = new CreateFolderCommand("newFolder", 3);
            var handlerSetup = CreateFoldersCommandHandlerFactory.CreateSetup(
                exists: false);
            var handler = handlerSetup.Handler;

            Should.Throw<ParentFolderDoesNotExistRequestException>(
                async () => await handler.Handle(command, CancellationToken.None));
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void CreateFolderCommand_NameIsNullOrEmpty_ThrowsArgumentException(string name)
        {
            Should.Throw<ArgumentException>(() => new CreateFolderCommand(name, 3));
        }
    }
}
