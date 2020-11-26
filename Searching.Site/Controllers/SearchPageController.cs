using Searching.Site.Models;
using System.Web.Mvc;
using Umbraco.Web.Models;
using Umbraco.Web.Mvc;
using Searching.Site.Models.ViewModels;
using Searching.Site.Services;

namespace Searching.Site.Controllers
{
    public class SearchPageController : RenderMvcController
    {
        private readonly ISearchService _searchService;

        public SearchPageController(ISearchService searchService)
        {
            _searchService = searchService;
        }

        public ActionResult Index(ContentModel model, string query, string page, string category)
        {
            var searchPageModel = new SearchContentModel(model.Content);

            var searchViewModel = new SearchViewModel()
            {
                Query = query,
                Category = category,
                Page = page
            };

            if(!int.TryParse(page, out var pageNumber))
            {
                pageNumber = 1;
            }

            var searchResults = _searchService.GetPageOfContentSearchResults(query, category,
                pageNumber, out var totalItemCount, null);

            searchPageModel.SearchViewModel = searchViewModel;
            searchPageModel.SearchResults = searchResults;
            
            return CurrentTemplate(searchPageModel);
        }
    }
}