using System.Web;
using System.Web.Mvc;

namespace EpiDemo.Web.Features.ReactComponents
{
    public static class ReactHtmlHelperExtensions
    {
        public static IHtmlString React(this HtmlHelper html, string componentName)
        {
            return global::React.Web.Mvc.HtmlHelperExtensions.React(html, componentName, new object());
        }

        public static IHtmlString React<T>(this HtmlHelper html, string componentName, ComponentProps<T> props)
        {
            return global::React.Web.Mvc.HtmlHelperExtensions.React(html, componentName, props ?? new object());
        }

        public static IHtmlString ReactWithModel<T>(this HtmlHelper html, string componentName, T model)
        {
            return global::React.Web.Mvc.HtmlHelperExtensions.React(html, componentName, new ComponentProps<T>(model));
        }
    }
}