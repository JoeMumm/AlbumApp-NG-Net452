using AlbumApp.Core.Common.Core;
using Autofac;
using AlbumApp.Core.Common.Contracts;

namespace AlbumApp.Client.Proxies
{
  public class ServiceFactory : IServiceFactory
  {
    T IServiceFactory.CreateClient<T>()
    { return (T)ObjectBase.Container.Resolve(typeof(T)); }
  }
}
