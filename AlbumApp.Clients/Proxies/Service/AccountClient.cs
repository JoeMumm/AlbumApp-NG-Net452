using AlbumApp.Client.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using AlbumApp.Client.Entities;
using AlbumAppCore.Common.ServiceModel;

namespace AlbumApp.Client.Proxies
{
  public class AccountClient : UserClientBase<IAccountService>, IAccountService
  {
    public Account GetCustomerAccountInfo(string loginEmail)
    {
      return Channel.GetCustomerAccountInfo(loginEmail);
    }

    public void UpdateCustomerAccountInfo(Account account)
    {
      Channel.UpdateCustomerAccountInfo(account);
    }
  }
}
