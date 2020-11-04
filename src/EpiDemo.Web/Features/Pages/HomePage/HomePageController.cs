using System.Web.Mvc;
using EPiServer.Web.Mvc;

namespace EpiDemo.Web.Features.Pages
{
    public class HomePageController : PageController<HomePage>
    {
        public ActionResult Index(HomePage currentContent)
        {
            return View("HomePage.cshtml", currentContent);
        }
    }
}