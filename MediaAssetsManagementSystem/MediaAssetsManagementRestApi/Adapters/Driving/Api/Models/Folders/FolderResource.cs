using Api.Models.Resources;
using System;

namespace Api.Models.Folders
{
    public class FolderResource : Resource
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }

        public FolderResource() { }

        public FolderResource(
            ApplicationServices.Requests.Queries.Folders.ReadFolder.FolderResponse folderResult)
        {
            Id = folderResult.Id;
            CreationDate = folderResult.CreationDate;
            Name = folderResult.Name;
            ParentId = folderResult.ParentId;
        }

        public FolderResource(
            ApplicationServices.Requests.Queries.Folders.ReadFolders.FolderResponse folderResult)
        {
            Id = folderResult.Id;
            CreationDate = folderResult.CreationDate;
            Name = folderResult.Name;
            ParentId = folderResult.ParentId;
        }
    }
}
