using ApplicationServices.Requests.Exceptions;
using MediatR;

namespace ApplicationServices.Requests.Queries.ImageAssets.ReadImageAsset
{
    /// <summary>
    /// Retrieves an existing Image Asset referenced by its identifier.
    /// </summary>
    /// <exception cref="NotFoundRequestException"
    public class ReadImageAssetQuery : IRequest<ImageAssetResponse>
    {
        public int Id { get; set; }

        public ReadImageAssetQuery(int id)
        {
            Id = id;
        }
    }
}
