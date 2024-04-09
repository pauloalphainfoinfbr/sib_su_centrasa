using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using BootstrapMvcSample.Controllers;
using NavigationRoutes;
using SIB.Web.Controllers;

namespace BootstrapMvcSample
{
    public class ExampleLayoutsRouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            //routes.MapNavigationRoute<HomeController>("Automatic Scaffolding", c => c.Index());

            routes.MapNavigationRoute<ExampleLayoutsController>("Log Off", c => c.LogOff())
                  //.AddChildRoute<ExampleLayoutsController>("Log Off", c => c.LogOff())
                  //.AddChildRoute<ExampleLayoutsController>("Fluid", c => c.Fluid())
                  //.AddChildRoute<ExampleLayoutsController>("Sign In", c => c.SignIn())
                ;
        }
    }
}
