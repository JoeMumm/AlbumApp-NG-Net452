using AlbumApp.Web.Core;
using AlbumApp.Web.Models;
using System.Web.Mvc;

namespace AlbumApp.Web.Controllers.MVC
{

  [RoutePrefix("account")]
  //[RequireHttps]
  public class AccountController : ViewControllerBase
  {
    ISecurityAdapter _SecurityAdapter;

    public AccountController(ISecurityAdapter securityAdapter)
    { _SecurityAdapter = securityAdapter; }

    [HttpGet]
    public ActionResult Login(string  returnUrl)
    { return View(new AccountLoginModel { ReturnUrl = returnUrl }); }

    [HttpGet]
    [Route("logout")]
    public ActionResult Logout()
    { _SecurityAdapter.LogOff();
      return RedirectToAction("Index", "Home"); }

    [HttpGet]
    public ActionResult Register()
    { return View(); }

  }
}