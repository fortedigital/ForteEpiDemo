using EPiServer.Core;
using EPiServer.DataAnnotations;

namespace EpiDemo.Web.Features.Pages
{
    [ContentType(GUID = "1D6BDD99-4152-415C-994E-1B7401FEF07F", DisplayName = "Home page",
        AvailableInEditMode = false)]
    public class HomePage: PageData
    {
        public virtual ContentArea Content { get; set; }
    }
}