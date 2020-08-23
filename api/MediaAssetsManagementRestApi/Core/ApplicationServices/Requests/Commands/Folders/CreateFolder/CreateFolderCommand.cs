using ApplicationServices.Requests.Commands.Folders.CreateFolder.Exceptions;
using ApplicationServices.Requests.Exceptions;
using EnsureThat;
using MediatR;

namespace ApplicationServices.Requests.Commands.Folders.CreateFolder
{
    /// <summary>
    /// Creates a new Folder
    /// </summary>
    /// <exception cref="FolderNameMustBeUniqueInParentRequestException">Thrown if folder name is not unique under the same parent.</exception>
    /// <exception cref="ParentFolderDoesNotExistRequestException">Thrown if parent folder does not exist.</exception>
    public class CreateFolderCommand : IRequest<int>
    {
        public string Name { get; set; }
        public int? ParentId { get; set; }

        public CreateFolderCommand(string name, int? parentId = null)
        {
            EnsureArg.IsNotNullOrEmpty(name, nameof(name));
            Name = name;

            ParentId = parentId;
        }
    }
}
