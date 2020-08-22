using EnsureThat;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Api.Models.Resources
{
    public abstract class Resource
    {
        private readonly List<Link> links = new List<Link>();

        [JsonProperty(Order = 100)]
        public IEnumerable<Link> Links { get { return links; } }

        protected Resource()
        {
            links = new List<Link>();
        }

        public void AddLink(Link link)
        {
            EnsureArg.IsNotNull(link, "link");
            links.Add(link);
        }

        public void AddLinks(params Link[] links)
        {
            Array.ForEach(links, link => AddLink(link));
        }
    }
}
