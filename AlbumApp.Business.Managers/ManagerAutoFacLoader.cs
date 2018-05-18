using AlbumApp.Core.Common.Contracts;
using AlbumApp.Data;
using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlbumApp.Business.Managers
{
  public static class ManagerAutoFacLoader
  {
    public static IContainer Init() {  
      var builder = Builder();
      return builder.Build();

      ////builder.RegisterGeneric(typeof(DataRepositoryFactory<>))
      ////  .As(typeof(IDataRepositoryFactory<>))

      ////builder.RegisterType<DataRepositoryFactory>()
      ////  .As<IDataRepositoryFactory>()
      ////  .InstancePerLifetimeScope();

      //// Register your dependencies.
      //builder.RegisterAssemblyTypes(typeof(AccountRepository).Assembly)
      //  .AsImplementedInterfaces().InstancePerDependency();
      ////.InstancePerLifetimeScope();
      ////.Where(r => r.Name.EndsWith("Repository"))
      //// Register your dependencies.
      //builder.RegisterAssemblyTypes(typeof(AccountManager).Assembly);
      ////.AsImplementedInterfaces().InstancePerDependency();
      //builder.RegisterType<DataRepositoryFactory>().As<IDataRepositoryFactory>() //.OnActivating(e => e.Parameters.CartManager(new DataRepositoryFactory()));
      //  .AsImplementedInterfaces().InstancePerDependency();
      ////AutofacHostFactory // container;
    }

    public static ContainerBuilder Builder()
    {
      var builder = new ContainerBuilder();
      builder.RegisterAssemblyTypes(typeof(AccountRepository).Assembly)
        .AsImplementedInterfaces().InstancePerDependency();
      //builder.RegisterAssemblyTypes(typeof(AccountManager).Assembly);
      //builder.RegisterAssemblyTypes(typeof(InventoryManager).Assembly);
      builder.RegisterType<DataRepositoryFactory>() //  .AsSelf()
        .As<IDataRepositoryFactory>();

      return builder;




      ////.AsImplementedInterfaces().InstancePerDependency();  // doesn't work
      //builder.RegisterAssemblyTypes(typeof(DataRepositoryFactory).Assembly)
      //  .AsImplementedInterfaces().InstancePerDependency();
      //builder.RegisterAssemblyTypes(typeof(InventoryClient).Assembly);
      //.AsImplementedInterfaces().InstancePerDependency();
      //builder
      //  .Register(c => c.Resolve<ChannelFactory<InventoryManager>>().CreateChannel())
      //  .As<InventoryManager>()
      //  .UseWcfSafeRelease();

      //builder.RegisterType<DataRepositoryFactory>().As<IDataRepositoryFactory>() //; //.OnActivating(e => e.Parameters.CartManager(new DataRepositoryFactory()));
      // .AsImplementedInterfaces()
      // .InstancePerDependency();
      //builder.RegisterType<InventoryManager>().As<IInventoryService>()
      //  .UsingConstructor(typeof(IDataRepositoryFactory));


      //.InstancePerLifetimeScope();
      //.Where(r => r.Name.EndsWith("Repository"))
      // Register your dependencies.
      //builder.Register(r => new AccountManager(r.Resolve<IDataRepositoryFactory>()))
      // .AsImplementedInterfaces().InstancePerDependency();
      //builder.Register(r => new CartManager(r.Resolve<IDataRepositoryFactory>()))
      // .AsImplementedInterfaces().InstancePerDependency();
    }
  }
}
