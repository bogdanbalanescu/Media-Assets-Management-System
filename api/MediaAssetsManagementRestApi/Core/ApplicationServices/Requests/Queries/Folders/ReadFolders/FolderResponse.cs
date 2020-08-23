using ApplicationServices.Ports.Persistence.DTOs.Folders;
using System;

namespace ApplicationServices.Requests.Queries.Folders.ReadFolders
{
    public class FolderResponse
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }

        public FolderResponse(FolderDto folder)
        {
            Id = folder.Id;
            CreationDate = folder.CreationDate;
            Name = folder.Name;
            ParentId = folder.ParentId;
        }
    }
}
