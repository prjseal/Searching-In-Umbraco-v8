using Searching.Site.Models;
using System.Web.Mvc;
using Umbraco.Web.Models;
using Umbraco.Web.Mvc;
using Searching.Site.Models.ViewModels;

namespace Searching.Site.Controllers
{
    public class SearchPageController : RenderMvcController
    {
        public ActionResult Index(ContentModel model, string query, string page, string category)
        {
            var searchPageModel = new SearchContentModel(model.Content);

            var searchViewModel = new SearchViewModel()
            {
                Query = query,
                Category = category,
                Page = page
            };

            searchPageModel.SearchViewModel = searchViewModel;
            
            return CurrentTemplate(searchPageModel);
        }
    }
}