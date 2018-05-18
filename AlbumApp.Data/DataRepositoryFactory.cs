using Autofac;
using AlbumApp.Core.Common.Contracts;
using AlbumApp.Core.Common.Core;

namespace AlbumApp.Data
{
  public class DataRepositoryFactory : IDataRepositoryFactory
  {
    T IDataRepositoryFactory.GetDataRepository<T>()
    { return (T) ObjectBase.Container.Resolve(typeof(T)); }
  }
}