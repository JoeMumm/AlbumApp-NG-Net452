using AlbumApp.Business.Entities;
using AlbumApp.Data;
using AlbumApp.Web.Core;
using AlbumApp.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AlbumApp.Web.Security
{
  [Authorize]
  public class SecurityAdapter : ISecurityAdapter {

    private ApplicationSignInManager _signInManager;
    private ApplicationUserManager _userManager;

    private AlbumContext _db;
    public SecurityAdapter() { _db = new AlbumContext(); }
    public SecurityAdapter(AlbumContext db)
    { _db = db; }

    public ApplicationSignInManager SignInManager {
      get { return _signInManager ??
         HttpContext.Current.GetOwinContext().Get<ApplicationSignInManager>(); }
      private set { _signInManager = value; } }


    public ApplicationUserManager UserManager {
      get { return _userManager ??
          HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
      private set { _userManager = value; } }

    // async
    public bool Register(string password, AccountRegisterModel accountRegisterModel) {
      var accountId = (from e in _db.Users orderby e.AccountSet.AccountId descending select e.AccountSet.AccountId).FirstOrDefault();
      Account account = new Account { FirstName = accountRegisterModel.FirstName,
        LastName = accountRegisterModel.LastName, Address = accountRegisterModel.Address, City = accountRegisterModel.City,
        State = accountRegisterModel.State, ZipCode = accountRegisterModel.ZipCode, CreditCard = accountRegisterModel.CreditCard,
        ExpDate = accountRegisterModel.ExpDate, AccountId = accountId + 1 };
      var user = new ApplicationUser { Email = accountRegisterModel.LoginEmail,
        UserName = accountRegisterModel.LoginEmail, AccountSet = account };
      var result = UserManager.Create(user, password);
      if (result.Succeeded) return true;
      else return false; }

    public bool Login(string email, string password, bool rememberMe) {
      var user = UserManager.FindByEmail(email);
      if (user == null) return false;
      var result = SignInManager.PasswordSignIn(user.UserName, password, rememberMe,
              shouldLockout: false);
      if (result == SignInStatus.Success)
      { return true;
      }
      else return false; }

    public int GetCartCount() {
      var cartCount = 0;
      var accountId = GetAccountId();
      if (accountId > 0) {
        cartCount = _db.CartItemSet.Where(c => c.AccountId == accountId && c.OrderId == 1)
          .Sum(c => (int?) c.Quantity) ?? 0; }
      return cartCount; }

    public int GetAccountId(){ var accountId = 0;
      var userId = HttpContext.Current.User.Identity.GetUserId();
      if (userId != null)
        accountId = (from a in _db.Users where a.Id == userId select a.AccountSet.AccountId).SingleOrDefault();
      return accountId;  }

    public Account GetUserInfo() { var user = new Account();
      var userId = HttpContext.Current.User.Identity.GetUserId();
      if (userId != null)
        user = (from a in _db.Users where a.Id == userId select a.AccountSet).SingleOrDefault();
      return user; }


    public void LogOff()
    { AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie); }

    private IAuthenticationManager AuthenticationManager
    { get { return HttpContext.Current.GetOwinContext().Authentication; } }

    public bool UserExists(string loginEmail) {
      var user = UserManager.FindByName(loginEmail);
      if (user == null) return false;
      return true; }

    public void Update(Account account)
    {

    }
  }

}
