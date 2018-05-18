using AlbumApp.Business.Entities;
using AlbumApp.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AlbumApp.Web.Core
{
  public interface ISecurityAdapter
  {
    bool Register( 
      string password, AccountRegisterModel accountRegisterModel);

    bool Login( 
      string email, string password, bool rememberMe); 

    void LogOff();

    bool UserExists(string loginEmail); 

    int GetAccountId();

    Account GetUserInfo();

    int GetCartCount();
  }
}
