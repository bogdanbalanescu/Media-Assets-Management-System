using System.Collections.Generic;

namespace ApplicationServices.Requests.Pagination
{
    public class PaginatedResult<T>
    {
        public IReadOnlyCollection<T> Items { get; set; }
        public string NextPageToken { get; set; }

        public PaginatedResult(IReadOnlyCollection<T> items, string nextPageToken)
        {
            Items = items;
            NextPageToken = nextPageToken;
        }
    }
}
