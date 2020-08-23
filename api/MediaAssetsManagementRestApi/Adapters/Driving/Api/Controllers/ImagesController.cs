using Api.Controllers.ErrorHandling;
using Api.Controllers.HypermediaLinksEnricher;
using Api.Models.ImageAssets;
using Api.Models.Resources;
using Api.Validators;
using Api.Validators.Exceptions;
using ApplicationServices.Ports.MediaAssetsPersistence;
using ApplicationServices.Requests.Commands.ImageAssets.CreateImageAsset;
using ApplicationServices.Requests.Exceptions;
using ApplicationServices.Requests.Queries.ImageAssets.ReadImageAsset;
using ApplicationServices.Requests.Queries.ImageAssets.ReadImageAssets;
using Azure.Storage.Blobs;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImagesController : ControllerBase
    {
        private readonly ILogger<ImagesController> _logger;
        private readonly IMediator mediator;
        private readonly BlobContainerClient blobContainerClient;

        internal ImagesController() { }
        public ImagesController(ILogger<ImagesController> logger, IMediator mediator, IMediaAssetsBlobContainerFactory mediaAssetsBlobContainerFactory)
        {
            _logger = logger;
            this.mediator = mediator;
            blobContainerClient = mediaAssetsBlobContainerFactory.GetContainerClient();
        }

        [HttpGet("", Name = "GetImages")]
        public async Task<ActionResult<ResourceCollection<ImageAssetResource>>> GetAsync(
            [FromQuery] int folderId, [FromQuery] int limit = 100, [FromQuery] string nextPageToken = "")
        {
            try
            {
                var query = new ReadImageAssetsQuery(folderId, limit, nextPageToken);
                var response = await mediator.Send(query);

                var imageAssets = response.Items.Select(x => new ImageAssetResource(x)).ToArray();
                foreach (var imageAsset in imageAssets)
                    imageAsset.EnrichWithLinks(this);

                var imageAssetsCollection = new ResourceCollection<ImageAssetResource>(imageAssets, response.NextPageToken);
                imageAssetsCollection.AddLinks(
                    new Link("self", this.Url.Link("GetImages", new { FolderId = folderId, Limit = limit, NextPageToken = nextPageToken })),
                    response.NextPageToken != null
                        ? new Link("next", this.Url.Link("GetImages", new { FolderId = folderId, Limit = limit, NextPageToken = response.NextPageToken }))
                        : new Link("next", null),
                    new Link("post", this.Url.Link("CreateImage", null))
                    );

                return imageAssetsCollection;
            }
            catch (InvalidNextPageTokenRequestException)
            {
                return BadRequest(new { Message = "The provided Next Page Token is invalid." });
            }
        }

        [HttpGet("{id}", Name = "GetActualImage")]
        public async Task<ActionResult> Get(int id)
        {
            var query = new ReadImageAssetQuery(id);
            var response = await mediator.Send(query);
            var blobClient = blobContainerClient.GetBlobClient(response.Guid.ToString());
            var imageStream = blobClient.OpenRead();

            return new FileStreamResult(imageStream, new MediaTypeHeaderValue(response.ContentType));
        }

        //[HttpGet("{id}", Name = "GetImage")]
        //public async Task<ActionResult<ImageAssetResource>> Get(int id)
        //{
        //    try
        //    {
        //        var query = new ReadImageAssetQuery(id);
        //        var response = await mediator.Send(query);
        //        var imageAssetResource = new ImageAssetResource(response);
        //        imageAssetResource.EnrichWithLinks(this);

        //        return imageAssetResource;
        //    }
        //    catch (NotFoundRequestException)
        //    {
        //        return NotFound();
        //    }
        //}

        [HttpPost("", Name = "CreateImage")]
        public async Task<ActionResult<ImageAssetResource>> Post(
            [FromForm] IFormFile imageFormFile, [FromForm] int folderId)
        {
            var formFileValidator = new FormFileValidator();
            try
            {
                formFileValidator.ValidateForImage(imageFormFile);

                var command = new CreateImageAssetCommand(imageFormFile.OpenReadStream(), imageFormFile.FileName, imageFormFile.ContentType, folderId);
                var imageId = await mediator.Send(command);

                var query = new ReadImageAssetQuery(imageId);
                var response = await mediator.Send(query);

                var imageAssetResource = new ImageAssetResource(response);
                imageAssetResource.EnrichWithLinks(this);

                return imageAssetResource;
            }
            catch (FileIsEmptyRequestException)
            {
                return this.BadRequest(ApiErrors.FileIsEmpty);
            }
            catch (FileNameContainsInvalidCharactersRequestException)
            {
                return this.BadRequest(ApiErrors.FileContainsInvalidCharacters);
            }
            catch (FileExtensionIsNotAcceptedRequestException)
            {
                return this.BadRequest(ApiErrors.FileExtensionIsNotAccepted);
            }
            catch (FileContentTypeIsNotAcceptedRequestException)
            {
                return this.BadRequest(ApiErrors.FileContentTypeIsNotAccepted);
            }
        }
    }
}
