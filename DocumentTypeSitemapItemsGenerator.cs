using System;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Core.Models;

namespace Tekhub.Umbraco.SitemapGenerator
{
    public class DocumentTypeSitemapItemsGenerator
    {
        protected readonly IPublishedContent Content;
        private readonly Func<string, bool> _isCrawlable;

        public DocumentTypeSitemapItemsGenerator(IPublishedContent content, Func<string, bool> isCrawlable)
        {
            Content = content;
            _isCrawlable = isCrawlable;
        }

        public virtual IEnumerable<SitemapItem> GetSitemapItems()
        {
            return new[] { GetSitemapItem() };
        }

        protected SitemapItem GetSitemapItem()
        {
            return _isCrawlable(Content.DocumentTypeAlias) ? new SitemapItem(Content) : null;
        }
    }
}
