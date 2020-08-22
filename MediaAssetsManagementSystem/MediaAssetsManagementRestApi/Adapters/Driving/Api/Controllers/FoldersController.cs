using Api.Controllers.ErrorHandling;
using Api.Controllers.HypermediaLinksEnricher;
using Api.Models.Folders;
using Api.Models.Resources;
using ApplicationServices.Requests.Commands.Folders.CreateFolder;
using ApplicationServices.Requests.Commands.Folders.CreateFolder.Exceptions;
using ApplicationServices.Requests.Exceptions;
using ApplicationServices.Requests.Queries.Folders.ReadFolder;
using ApplicationServices.Requests.Queries.Folders.ReadFolders;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FoldersController : ControllerBase
    {
        private readonly ILogger<FoldersController> _logger;
        private readonly IMediator mediator;

        public FoldersController(ILogger<FoldersController> logger, IMediator mediator)
        {
            _logger = logger;
            this.mediator = mediator;
        }

        [HttpGet("", Name = "GetFolders")]
        public async Task<ActionResult<ResourceCollection<FolderResource>>> GetAsync(
            [FromQuery] int limit = 100, [FromQuery] string nextPageToken = "")
        {
            try
            {
                var query = new ReadFoldersQuery(limit, nextPageToken);
                var response = await mediator.Send(query);

                var folders = response.Items.Select(x => new FolderResource(x)).ToArray();
                foreach (var folder in folders)
                    folder.EnrichWithLinks(this);

                var foldersCollection = new ResourceCollection<FolderResource>(folders, response.NextPageToken);
                foldersCollection.AddLinks(
                    new Link("self", this.Url.Link("GetFolders", new { Limit = limit, NextPageToken = nextPageToken })),
                    response.NextPageToken != null
                        ? new Link("next", this.Url.Link("GetFolders", new { Limit = limit, NextPageToken = response.NextPageToken }))
                        : new Link("next", null),
                    new Link("post", this.Url.Link("CreateFolder", null))
                    );

                return foldersCollection;
            }
            catch (InvalidNextPageTokenRequestException)
            {
                return BadRequest(new { Message = "The provided Next Page Token is invalid." });
            }
        }

        [HttpGet("{id}", Name = "GetFolder")]
        public async Task<ActionResult<FolderResource>> Get(int id)
        {
            try
            {
                var query = new ReadFolderQuery(id);
                var response = await mediator.Send(query);
                var folderResource = new FolderResource(response);
                folderResource.EnrichWithLinks(this);

                return folderResource;
            }
            catch (NotFoundRequestException)
            {
                return NotFound();
            }
        }

        [HttpPost("", Name = "CreateFolder")]
        public async Task<ActionResult<FolderResource>> Post([FromBody] FolderResource folder)
        {
            try
            {
                var command = new CreateFolderCommand(folder.Name);
                var folderId = await mediator.Send(command);

                var query = new ReadFolderQuery(folderId);
                var response = await mediator.Send(query);

                var folderResource = new FolderResource(response);
                folderResource.EnrichWithLinks(this);

                return folderResource;
            }
            catch (FolderNameMustBeUniqueInParentRequestException)
            {
                return this.BadRequest(ApiErrors.FolderNameMustBeUniqueInParent);
            }
        }

        [HttpGet("{id}/subfolders", Name = "GetSubfolders")]
        public async Task<ActionResult<ResourceCollection<FolderResource>>> GetAsync(
            int id, [FromQuery] int limit = 100, [FromQuery] string nextPageToken = "")
        {
            try
            {
                var query = new ReadFoldersQuery(limit, nextPageToken, id);
                var response = await mediator.Send(query);

                var folders = response.Items.Select(x => new FolderResource(x)).ToArray();
                foreach (var folder in folders)
                    folder.EnrichWithLinks(this);

                var foldersCollection = new ResourceCollection<FolderResource>(folders, response.NextPageToken);
                foldersCollection.AddLinks(
                    new Link("self", this.Url.Link("GetSubfolders", new { Id = id, Limit = limit, NextPageToken = nextPageToken })),
                    response.NextPageToken != null
                        ? new Link("next", this.Url.Link("GetSubfolders", new { Id = id, Limit = limit, NextPageToken = response.NextPageToken }))
                        : new Link("next", null),
                    new Link("post", this.Url.Link("CreateSubfolder", new { Id = id }))
                    );

                return foldersCollection;
            }
            catch (InvalidNextPageTokenRequestException)
            {
                return BadRequest(new { Message = "The provided Next Page Token is invalid." });
            }
        }

        [HttpPost("{id}/subfolders", Name = "CreateSubfolder")]
        public async Task<ActionResult<FolderResource>> Post(int id, [FromBody] FolderResource folder)
        {
            try
            {
                var command = new CreateFolderCommand(folder.Name, id);
                var folderId = await mediator.Send(command);

                var query = new ReadFolderQuery(folderId);
                var response = await mediator.Send(query);

                var folderResource = new FolderResource(response);
                folderResource.EnrichWithLinks(this);

                return folderResource;
            }
            catch (FolderNameMustBeUniqueInParentRequestException)
            {
                return this.BadRequest(ApiErrors.FolderNameMustBeUniqueInParent);
            }
        }
    }
}
