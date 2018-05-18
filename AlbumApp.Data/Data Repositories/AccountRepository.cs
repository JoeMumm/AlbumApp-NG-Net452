using AlbumApp.Business.Entities;
using AlbumApp.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AlbumApp.Data
{
  public class AccountRepository : DataRepositoryBase<Account>, IAccountRepository
  {
    protected override Account AddEntity(AlbumContext entityContext, Account entity) {
      throw new NotImplementedException();
    } 

    protected override Account UpdateEntity(AlbumContext entityContext, Account entity) {
      throw new NotImplementedException();
    }

    protected override IEnumerable<Account> GetEntities(AlbumContext entityContext)
    {
      throw new NotImplementedException();
    }

    protected override Account GetEntity(AlbumContext entityContext, int id) {
      return (from a in entityContext.Users
              where a.AccountSet.AccountId == id
              select a.AccountSet).FirstOrDefault(); }

    public Account GetByLogin(string login) {
      using (AlbumContext entityContext = new AlbumContext()) {
        return (from a in entityContext.Users
                where a.AccountSet.User.Email == login
                select a.AccountSet).FirstOrDefault(); } }


  }
}

