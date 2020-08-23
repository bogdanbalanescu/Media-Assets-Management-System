using Api.Models.ImageAssets;
using Api.Models.Resources;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.HypermediaLinksEnricher
{
    public static class ImageAssetsHypermediaLinksEnricher
    {
        public static void EnrichWithLinks(this ImageAssetResource image, ControllerBase controller)
        {
            image.AddLinks(
                new Link("self", controller.Url.Link("GetImage", new { Id = image.Id }))
                );
        }
    }
}
