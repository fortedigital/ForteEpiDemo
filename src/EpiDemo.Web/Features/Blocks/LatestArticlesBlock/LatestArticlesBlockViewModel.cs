using System.Collections.Generic;
using EpiDemo.Web.Features.Pages;

namespace EpiDemo.Web.Features.Blocks
{
    public class LatestArticlesBlockViewModel
    {
        public string Heading { get; set; }
        public IEnumerable<ArticlePage> Articles { get; set; }
    }
}