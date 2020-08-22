using ApplicationServices.Requests.Commands.Folders.CreateFolder.Exceptions;
using Domain.Aggregates.Folders;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationServices.Requests.Commands.Folders.CreateFolder
{
    internal class CreateFolderCommandHandler : IRequestHandler<CreateFolderCommand, int>
    {
        private readonly IFoldersRepository foldersRepository;

        public CreateFolderCommandHandler(IFoldersRepository foldersRepository)
        {
            this.foldersRepository = foldersRepository;
        }

        public async Task<int> Handle(CreateFolderCommand request, CancellationToken cancellationToken)
        {
            if (!await foldersRepository.IsUniqueInParent(request.Name, request.ParentId))
                throw new FolderNameMustBeUniqueInParentRequestException();

            var folder = new Folder(request.Name, request.ParentId);
            var folderId = await foldersRepository.AddAsync(folder);

            return folderId;
        }
    }
}
