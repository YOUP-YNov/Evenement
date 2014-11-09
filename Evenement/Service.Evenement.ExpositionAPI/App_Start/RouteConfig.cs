using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Service.Evenement.ExpositionAPI
{
    /// <summary>
    /// configuration des routes
    /// </summary>
    public class RouteConfig
    {
        /// <summary>
        /// enregistrement des routes
        /// </summary>
        /// <param name="routes">listes de routes</param>
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
