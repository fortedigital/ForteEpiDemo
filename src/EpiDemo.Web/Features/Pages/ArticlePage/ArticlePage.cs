using System;
using System.ComponentModel.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;

// ReSharper disable Mvc.TemplateNotResolved

namespace EpiDemo.Web.Features.Pages
{
    [ContentType(GUID = "33C2B378-49C8-47C1-A8D3-8B4ECEC6EC63", DisplayName = "Article")]
    [AvailableContentTypes(Availability.Specific, IncludeOn = new[]
    {
        typeof(HomePage)
    })]
    public class ArticlePage: PageData
    {
        [Display(Name = "My title", Description = "Edit title here", Prompt = "Prompt for the property", Order = 20)]
        public virtual string Title { get; set; }
        [Display(Order = 10)]
        public virtual DateTime DateProperty { get; set; }
        [Display(GroupName = "Custom")]
        public virtual int IntProperty { get; set; }
        
        public virtual ContentArea RelatedContent { get; set; } 
        
        public virtual ContentReference AnotherArticle { get; set; }
        
        public virtual XhtmlString Body { get; set; }
    }
}