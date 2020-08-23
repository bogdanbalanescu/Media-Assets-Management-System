using ApplicationServices.Requests.Exceptions;
using MediatR;

namespace ApplicationServices.Requests.Queries.Folders.ReadFolder
{
    /// <summary>
    /// Retrieves an existing Folder referenced by its identifier.
    /// </summary>
    /// <exception cref="NotFoundRequestException"
    public class ReadFolderQuery : IRequest<FolderResponse>
    {
        public int Id { get; set; }

        public ReadFolderQuery(int id)
        {
            Id = id;
        }
    }
}
