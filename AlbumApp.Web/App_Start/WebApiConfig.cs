using System.Web.Http;
using AlbumApp.Web.Core;

namespace AlbumApp.Web
{
  public static class WebApiConfig
  {
    public static void Register(HttpConfiguration config)
    {
      //// Web API configuration and services
      //// Configure Web API to use only bearer token authentication.
      //config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

      config.MessageHandlers.Add(new SuppressRedirectHandler());

      // Web API routes
      config.MapHttpAttributeRoutes();
      
      config.Routes.MapHttpRoute(
          name: "DefaultApi",
          routeTemplate: "api/{controller}/{id}",
          defaults: new { id = RouteParameter.Optional } );
    }
  }
}
