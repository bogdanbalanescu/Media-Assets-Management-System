using ApplicationServices.Ports.Persistence.Exceptions;
using Newtonsoft.Json;
using System;
using System.Text;

namespace Persistence.Repositories.Models
{
    public class NextPageToken
    {
        public int NextPageIndex { get; }
        public bool HasNextPage { get; }

        public NextPageToken(int nextPageIndex, bool hasNextPage)
        {
            NextPageIndex = nextPageIndex;
            HasNextPage = hasNextPage;
        }

        public NextPageToken(string nextPageToken)
        {
            try
            {
                byte[] base64String = Convert.FromBase64String(nextPageToken);
                string decryptedNextPageToken = Encoding.ASCII.GetString(base64String);
                var nextPage = JsonConvert.DeserializeObject<NextPage>(decryptedNextPageToken);

                NextPageIndex = nextPage.NextPageIndex.Value;
            }
            catch (Exception ex)
            {
                throw new InvalidNextPageTokenRepositoryException($"Next Page Token {nextPageToken} is invalid with exception message: {ex.Message}");
            }
            if (NextPageIndex < 1)
            {
                throw new InvalidNextPageTokenRepositoryException($"Next Page Token {nextPageToken} is invalid with index: {NextPageIndex}");
            }
        }

        public override string ToString()
        {
            string nextPageToken = null;
            
            if (HasNextPage)
            {
                var nextPage = new NextPage { NextPageIndex = NextPageIndex + 1 };
                string nextPageJson = JsonConvert.SerializeObject(nextPage);

                byte[] nextPageAsBytes = Encoding.ASCII.GetBytes(nextPageJson);
                nextPageToken = Convert.ToBase64String(nextPageAsBytes);
            }

            return nextPageToken;
        }

        private class NextPage
        {
            [JsonProperty(Required = Required.Always)]
            public int? NextPageIndex { get; set; }
        }
    }
}
