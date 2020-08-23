using Domain.Aggregates.Folders;
using System;

namespace ApplicationServices.Ports.Persistence.DTOs.Folders
{
    public class FolderDto
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }

        public FolderDto(Folder folder)
        {
            Id = folder.Id;
            CreationDate = folder.CreationDate;
            Name = folder.Name;
            ParentId = folder.ParentId;
        }
    }
}
