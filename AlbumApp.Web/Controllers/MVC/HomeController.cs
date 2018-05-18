using AlbumApp.Web.Core;
using System.Web.Mvc;

namespace AlbumApp.Web.Controllers
{

  public class HomeController : Controller
  {
    private readonly ISecurityAdapter _securityAdapter;

    public HomeController(ISecurityAdapter securityAdapter)
    { _securityAdapter = securityAdapter; }

    public ActionResult Index() {
      ViewBag.Title = "Home Page";
      if (User != null) SetCart();
      return View(); }

    private void SetCart() { var cartCount = 0;
      cartCount = _securityAdapter.GetCartCount();
      Session["cart"] = cartCount; }

    public ActionResult ToBeImplemented()
    {
      ViewBag.Message = "To Be  Implemented";
      return View();
    }

  }
}
