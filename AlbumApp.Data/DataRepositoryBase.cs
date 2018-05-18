using AlbumApp.Core.Common.Contracts;
using AlbumApp.Core.Common.Data;

namespace AlbumApp.Data
{
  public abstract class DataRepositoryBase<T> : DataRepositoryBase<T, AlbumContext>
     where T : class, IIdentifiableEntity, new() { }
}
