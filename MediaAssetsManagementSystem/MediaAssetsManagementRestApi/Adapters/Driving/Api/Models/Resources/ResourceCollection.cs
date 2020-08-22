namespace Api.Models.Resources
{
    public class ResourceCollection<T> : Resource where T : Resource
    {
        public T[] Items { get; set; }
        public string ContinuationToken { get; set; }

        public ResourceCollection(T[] resources, string continuationToken)
        {
            Items = resources;
            ContinuationToken = continuationToken;
        }
    }
}
