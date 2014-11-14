using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Service.Evenement.ExpositionAPI
{
    /// <summary>
    /// configuration de la webAPI
    /// </summary>
    public static class WebApiConfig
    {
        /// <summary>
        /// enregistrement du template de routing
        /// </summary>
        /// <param name="config">HttpConfig</param>
        public static void Register(HttpConfiguration config)
        {
            // Configuration et services API Web
            config.EnableCors();

            // Itinéraires de l'API Web
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
