using Umbraco.Core.Models;
using Umbraco.Web;

namespace Tekhub.Umbraco.SitemapGenerator
{
    public class SitemapItem
    {
        public string Location { get; set; }
        public string LastModifiedDate { get; set; }

        public SitemapItem(IPublishedContent content)
        {
            Location = content.UrlWithDomain();
            LastModifiedDate = content.UpdateDate.ToString("s") + "+00:00";
        }

        public SitemapItem()
        {
        }
    }
}
