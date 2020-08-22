namespace ApplicationServices.Requests.Pagination
{
    public class PaginationDto
    {
        /// <summary>
        /// Limit for number of items in a page
        /// </summary>
        public int Limit { get; set; }

        /// <summary>
        /// Next Page Token
        /// </summary>
        public string NextPageToken { get; set; }
    }
}
