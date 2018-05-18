using AlbumApp.Client.Bootstrapper;
using AlbumApp.Web.Security;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;

namespace AlbumApp.Web
{
  public static class AutoFacConfig
  {
    public static void Configure()
    {
      // MVC setup documentation here:
      // http://autofac.readthedocs.io/en/latest/integration/mvc.html
      // WCF setup documentation here:
      // http://autofac.readthedocs.io/en/latest/integration/wcf.html
      //

      var builder = AutoFacLoader.Builder();
      
      builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
      builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly());

      builder.RegisterType<SecurityAdapter>().As<Core.ISecurityAdapter>() 
       .AsImplementedInterfaces().InstancePerDependency();
      var container = builder.Build();
      DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
      GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
    }
  }
}
