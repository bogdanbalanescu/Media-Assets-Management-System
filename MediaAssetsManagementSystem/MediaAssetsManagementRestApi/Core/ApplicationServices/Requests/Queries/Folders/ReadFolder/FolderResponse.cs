using Domain.Aggregates.Folders;
using System;

namespace ApplicationServices.Requests.Queries.Folders.ReadFolder
{
    public class FolderResponse
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }

        public FolderResponse(Folder folder)
        {
            Id = folder.Id;
            CreationDate = folder.CreationDate;
            Name = folder.Name;
            ParentId = folder.ParentId;
        }
    }
}
