namespace EpiDemo.Web.Features.Pages
{
    public class ArticlePageViewModel : PageViewModel<ArticlePage>
    {
        public ArticlePageViewModel(ArticlePage content) : base(content)
        {
        }
    }

    public class PageViewModel<T>
    {
        public PageViewModel(T content)
        {
            this.Content = content;
        }

        public T Content { get; }
    }
}