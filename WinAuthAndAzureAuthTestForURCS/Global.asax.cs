using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WinAuthAndAzureAuthTestForURCS.Utils;

namespace WinAuthAndAzureAuthTestForURCS
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        protected void Session_OnStart()
        {

            int returnStatus = (Int32)HttpStatusCode.OK;
            if (Session.Keys.Count == 0)
                returnStatus = URCSHelpers.setUserSession();

            Response.StatusCode = returnStatus;

        }
    }
}
