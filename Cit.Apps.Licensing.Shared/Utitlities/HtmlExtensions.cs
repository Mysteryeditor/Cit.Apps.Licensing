using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cit.Apps.Licensing.Shared.Utitlities
{
    public static class HtmlExtensions
    {
        public static string IsActive(this IHtmlHelper htmlHelper, string controller, string action)
        {
            var routeData = htmlHelper.ViewContext.RouteData;

            var routeAction = routeData.Values["action"].ToString();
            var routeController = routeData.Values["controller"].ToString();

            var returnActive = (controller == routeController && (action == routeAction || routeAction == "Details"));

            return returnActive ? "active" : "";
        }
    }
}
