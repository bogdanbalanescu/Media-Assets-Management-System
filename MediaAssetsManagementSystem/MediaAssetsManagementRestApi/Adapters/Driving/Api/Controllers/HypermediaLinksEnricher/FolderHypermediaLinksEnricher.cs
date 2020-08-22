using Api.Models.Folders;
using Api.Models.Resources;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.HypermediaLinksEnricher
{
    public static class FolderHypermediaLinksEnricher
    {
        public static void EnrichWithLinks(this FolderResource folder, ControllerBase controller)
        {
            folder.AddLinks(
                new Link("self", controller.Url.Link("GetFolder", new { Id = folder.Id })),
                new Link("subfolders", controller.Url.Link("GetSubfolders", new { Id = folder.Id })),
                new Link("post", controller.Url.Link("CreateSubfolder", new { Id = folder.Id })),
                new Link("create-image", controller.Url.Link("CreateImage", new { Id = folder.Id }))
                );
        }
    }
}
