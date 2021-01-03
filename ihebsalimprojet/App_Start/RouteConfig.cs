﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ihebsalimprojet
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}",
                defaults: new { controller = "Authentification", action = "Index" }
            );
            routes.MapRoute(
                    name : "Gestion du projet",
                     url: "{controller}/{action}",
                new { controller = "GestionProjet", action = "Index" });
        }
    }
}