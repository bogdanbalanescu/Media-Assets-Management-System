using ApplicationServices.Ports.Persistence;
using ApplicationServices.Ports.Persistence.Exceptions;
using ApplicationServices.Requests.Exceptions;
using ApplicationServices.Requests.Pagination;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationServices.Requests.Queries.Folders.ReadFolders
{
    internal class ReadFoldersQueryHandler : IRequestHandler<ReadFoldersQuery, PaginatedResult<FolderResponse>>
    {
        private readonly IFoldersQueriesRepository foldersQueriesRepository;

        public ReadFoldersQueryHandler(IFoldersQueriesRepository foldersQueriesRepository)
        {
            this.foldersQueriesRepository = foldersQueriesRepository;
        }

        public async Task<PaginatedResult<FolderResponse>> Handle(ReadFoldersQuery request, CancellationToken cancellationToken)
        {
            var paginationDto = new PaginationDto
            {
                Limit = request.Limit,
                NextPageToken = request.NextPageToken
            };

            try
            {
                var foldersPage = await foldersQueriesRepository.GetPageAsync(paginationDto, request.ParentId);
                var foldersResponse = foldersPage.Items.Select(note => new FolderResponse(note)).ToList();

                return new PaginatedResult<FolderResponse>(foldersResponse, foldersPage.NextPageToken);
            }
            catch (InvalidNextPageTokenRepositoryException)
            {
                throw new InvalidNextPageTokenRequestException();
            }
        }
    }
}
