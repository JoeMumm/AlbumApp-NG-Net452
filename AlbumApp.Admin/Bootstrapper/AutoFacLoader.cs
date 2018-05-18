using AlbumApp.Client.Bootstrapper;
using AlbumApp.Client.Proxies;
using AlbumApp.Core.Common.Contracts;
using Autofac;
using System.Reflection;

namespace AlbumApp.Admin.Bootstrapper
{
  public static class AutoFacLoader
  {
    public static IContainer Init() {
      var builder = Client.Bootstrapper.AutoFacLoader.Builder(); 
      builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
        .InNamespace("AlbumApp.Admin.ViewModels").SingleInstance();
       return builder.Build();
 }
  }
}
