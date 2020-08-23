using EnsureThat;
using MediatR;

namespace ApplicationServices.Requests.Commands.Folders.CreateFolder
{
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
