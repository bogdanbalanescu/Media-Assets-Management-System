using EnsureThat;

namespace Api.Models.Resources
{
    public class Link
    {
        public string Rel { get; private set; }
        public string Href { get; private set; }

        public Link(string relation, string href)
        {
            EnsureArg.IsNotNullOrEmpty(relation, "relation");

            Rel = relation;
            Href = href;
        }
    }
}
