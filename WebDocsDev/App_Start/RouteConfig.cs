using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebDocsDev
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            #region File Routes

            routes.MapRoute(
               name: "AllPublicDocuments",
               url: "{controller}/{action}/{id}",
               defaults: new { controller = "Files", action = "DisplayPublicDocs", id = UrlParameter.Optional }
           );
            routes.MapRoute(
               name: "AllUserDocuments",
               url: "{controller}/{action}/{id}",
               defaults: new { controller = "Files", action = "DisplayUserDocs", id = UrlParameter.Optional }
           );

            #endregion

            #region Notifications
            routes.MapRoute(
               name: "DispalyUserNotifications",
               url: "{controller}/{action}/{id}",
               defaults: new { controller = "Notifications", action = "DisplayUserNotifications", id = UrlParameter.Optional }
           );
            #endregion

           // #region Content Management 
           // routes.MapRoute(
           //    name: "DispalyUserNotifications",
           //    url: "{controller}/{action}/{id}",
           //    defaults: new { controller = "ContentManagement", action = "Download", id = UrlParameter.Optional }
           //);
           // #endregion
        }
    }
}
