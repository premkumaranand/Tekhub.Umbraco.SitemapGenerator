using System;
using System.Collections.Generic;
using Umbraco.Core;
using Umbraco.Core.Models;

namespace Tekhub.Umbraco.SitemapGenerator
{
    public class SitemapDocumentTypeMapper
    {
        private readonly Dictionary<string, Type> _customSitemapGeneratorMapping;
 
        public SitemapDocumentTypeMapper()
        {
            _customSitemapGeneratorMapping = new Dictionary<string, Type>();
        }

        public void AddCustomMapper(string documentTypeAlias, Type type)
        {
            if (type.BaseType != typeof(DocumentTypeSitemapItemsGenerator))
            {
                throw new Exception("Not a valid mapper");
            }

            _customSitemapGeneratorMapping[documentTypeAlias] = type;
        }

        public IEnumerable<SitemapItem> Map(IPublishedContent content, Func<string, bool> isCrawlable)
        {
            var docType = content.DocumentTypeAlias;
            
            DocumentTypeSitemapItemsGenerator generator;

            if (!_customSitemapGeneratorMapping.ContainsKey(docType))
            {
                generator = new DocumentTypeSitemapItemsGenerator(content, isCrawlable);
            }
            else
            {
                generator =
                    (DocumentTypeSitemapItemsGenerator)
                        Activator.CreateInstance(_customSitemapGeneratorMapping[docType], content, isCrawlable);
            }

            return generator.GetSitemapItems().WhereNotNull();
        } 
    }
}
