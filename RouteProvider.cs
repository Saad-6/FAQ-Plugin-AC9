using CommerceBuilder.Web.Routing;
using System.Web.Mvc;
using System.Web.Routing;

namespace FAQPlugin
{
    public class RouteProvider : IRouteProvider
    {
        public void RegisterRoutes(RouteCollection routes)
        {
            if (routes["Plugins_General_FAQPlugin_Admin"] == null)
            {
                var route = routes.MapRoute(
                    "Plugins_General_FAQPlugin_Admin",
                    "Admin/FAQ/{action}",
                    new { controller = "FAQAdmin", action = "Index" },
                    new[] { "FAQPlugin.Controllers" }
                );

                route.DataTokens["area"] = "Admin";
            }

            if (routes["Plugins_General_FAQPlugin_Retail"] == null)
            {
                var route = routes.MapRoute(
                    "Plugins_General_FAQPlugin_Retail",
                    "FAQRetail/{action}",
                    new { controller = "EPPRetail" },
                    new[] { "FAQPlugin.Controllers" }
                );
            }
        }
    }
}
