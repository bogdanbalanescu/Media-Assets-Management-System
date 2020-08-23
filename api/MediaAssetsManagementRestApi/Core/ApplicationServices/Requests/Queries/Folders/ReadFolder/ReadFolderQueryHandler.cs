using ApplicationServices.Requests.Exceptions;
using Domain.Aggregates.Folders;
using Domain.SharedKernel.Exceptions;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationServices.Requests.Queries.Folders.ReadFolder
{
    internal class ReadFolderQueryHandler : IRequestHandler<ReadFolderQuery, FolderResponse>
    {
        private readonly IFoldersRepository foldersRepository;

        public ReadFolderQueryHandler(IFoldersRepository foldersRepository)
        {
            this.foldersRepository = foldersRepository;
        }

        public async Task<FolderResponse> Handle(ReadFolderQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var folder = await foldersRepository.GetByIdAsync(request.Id);

                return new FolderResponse(folder);
            }
            catch (NotFoundRepositoryException)
            {
                throw new NotFoundRequestException();
            }
        }
    }
}
