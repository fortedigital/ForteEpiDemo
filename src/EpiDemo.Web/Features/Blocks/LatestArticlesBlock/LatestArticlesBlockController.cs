using System.Web.Mvc;
using EpiDemo.Web.Features.Pages;
using EPiServer;
using EPiServer.Core;
using EPiServer.Web.Mvc;

namespace EpiDemo.Web.Features.Blocks
{
    public class LatestArticlesBlockController : BlockController<LatestArticlesBlock>
    {
        private readonly IContentLoader loader;

        public LatestArticlesBlockController(IContentLoader loader)
        {
            this.loader = loader;
        }

        public override ActionResult Index(LatestArticlesBlock currentContent)
        {
            var articles = this.loader.GetChildren<ArticlePage>(ContentReference.StartPage);
            
            return this.PartialView("LatestArticlesBlock", new LatestArticlesBlockViewModel
            {
                Heading = currentContent.Heading,
                Articles = articles
            });
        }
    }
}