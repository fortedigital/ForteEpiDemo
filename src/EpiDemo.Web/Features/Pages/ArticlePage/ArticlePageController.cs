using System.Web.Mvc;
using EPiServer;
using EPiServer.Core;
using EPiServer.Data.Dynamic;
using EPiServer.Web.Mvc;

namespace EpiDemo.Web.Features.Pages
{
    public class ArticlePageController : PageController<ArticlePage>
    {
        private readonly IContentLoader contentLoader;

        public ArticlePageController(IContentLoader contentLoader)
        {
            this.contentLoader = contentLoader;
        }

        public ActionResult Index(ArticlePage currentContent)
        {
            
            return View("ArticlePage.cshtml", new ArticlePageViewModel(currentContent));
        }
    }
}