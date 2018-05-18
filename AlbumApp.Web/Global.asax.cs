﻿using System;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace AlbumApp.Web
{
  public class WebApiApplication : System.Web.HttpApplication
  {
    protected void Application_Start()
    {
      AreaRegistration.RegisterAllAreas();
      GlobalConfiguration.Configure(WebApiConfig.Register);
      FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
      RouteConfig.RegisterRoutes(RouteTable.Routes);
      BundleConfig.RegisterBundles(BundleTable.Bundles);

      AutoFacConfig.Configure();

    }

    protected void Application_EndRequest(Object sender, EventArgs e)
    {
      HttpApplication context = (HttpApplication) sender;
      context.Response.SuppressFormsAuthenticationRedirect = true;
    }
  }
}
