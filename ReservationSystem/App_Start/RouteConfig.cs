using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ReservationSystem
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // Ruta personalizada para la página de reservas
            routes.MapRoute(
                name: "Reservation",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Reservation", action = "Index" }
            );

            // Ruta personalizada para la página de salas
            routes.MapRoute(
                name: "Rooms",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Room", action = "Index" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
