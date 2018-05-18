using AlbumApp.Business.Managers;
using AlbumApp.Data;
using Autofac;

namespace AlbumApp.Business.Bootstrapper
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
      builder.RegisterAssemblyTypes(typeof(AccountRepository).Assembly)
        .AsImplementedInterfaces().InstancePerDependency();
      builder.RegisterAssemblyTypes(typeof(AccountManager).Assembly);
      
      return builder;
    }
  }
}
