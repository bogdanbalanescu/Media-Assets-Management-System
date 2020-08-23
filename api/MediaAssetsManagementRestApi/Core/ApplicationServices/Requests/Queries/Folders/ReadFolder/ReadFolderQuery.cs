using MediatR;

namespace ApplicationServices.Requests.Queries.Folders.ReadFolder
{
    public class ReadFolderQuery : IRequest<FolderResponse>
    {
        public int Id { get; set; }

        public ReadFolderQuery(int id)
        {
            Id = id;
        }
    }
}
