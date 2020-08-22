using MediatR;

namespace ApplicationServices.Requests.Queries.ImageAssets.ReadImageAsset
{
    public class ReadImageAssetQuery : IRequest<ImageAssetResponse>
    {
        public int Id { get; set; }

        public ReadImageAssetQuery(int id)
        {
            Id = id;
        }
    }
}
