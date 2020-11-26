using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web.Models;
using Searching.Site.Models.ViewModels;
using System.Collections.Generic;

namespace Searching.Site.Models
{
    public class SearchContentModel : ContentModel
    {
        public SearchContentModel(IPublishedContent content) : base(content)
        {

        }

        public SearchViewModel SearchViewModel { get; set; }

        public IEnumerable<IPublishedContent> SearchResults { get; set; }
    }
}