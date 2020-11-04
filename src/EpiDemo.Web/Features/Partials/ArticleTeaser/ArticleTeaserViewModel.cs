using EPiServer.Core;

namespace EpiDemo.Web.Features.Partials.ArticleTeaser
{
    public class ArticleTeaserViewModel
    {
        public string Title { get; set; }
        public ContentReference ArticleLink { get; set; }
    }
}