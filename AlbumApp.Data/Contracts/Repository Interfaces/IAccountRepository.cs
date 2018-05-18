using AlbumApp.Business.Entities;
using AlbumApp.Core.Common.Contracts;

namespace AlbumApp.Data.Contracts
{
  public interface IAccountRepository : IDataRepository<Account>
  {
    Account GetByLogin(string login);
  }
}
