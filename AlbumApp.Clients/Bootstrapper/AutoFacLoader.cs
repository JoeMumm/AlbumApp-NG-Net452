using AlbumApp.Client.Proxies;
using Autofac;

namespace AlbumApp.Client.Bootstrapper
{
  public static class AutoFacLoader
  {
    public static IContainer Init() { 
      var builder = Builder();
      return builder.Build();
    }

    public static ContainerBuilder Builder()
    {
      var builder = new ContainerBuilder();
      builder.RegisterAssemblyTypes(typeof(InventoryClient).Assembly)
        .AsImplementedInterfaces().InstancePerDependency();

      return builder;
    }

  }
}
