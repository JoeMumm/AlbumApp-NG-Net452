using AlbumApp.Client.Contracts;
using AlbumApp.Core.Common.Contracts;
using AlbumApp.Web.Core;
using System.Collections.Generic;
using System.Web.Mvc;

namespace AlbumApp.Web.Controllers
{
  [UsesDisposableService]
  [Authorize]
  public class OrderController : ViewControllerBase
  {
    private readonly IOrderService _orderService;
    private readonly ISecurityAdapter _securityAdapter;

    public OrderController(IOrderService orderService)
    { _orderService = orderService; }

    public OrderController(ISecurityAdapter securityAdapter)
    { _securityAdapter = securityAdapter; }

    public OrderController(IOrderService orderService, ISecurityAdapter securityAdapter)
    { _orderService = orderService; _securityAdapter = securityAdapter; }


    protected override void RegisterServices(List<IServiceContract> disposableServices)
    { disposableServices.Add(_orderService); }

    [HttpGet]
    public ActionResult Index()
    {
      return View();
    }

    [HttpGet]
    public ActionResult Detail(int orderId) {
      
      return View();
    }

    [HttpGet]
    public ActionResult Confirmation(int orderId)
    {
      SetCart();
      var order = _orderService.Get(orderId);
      return View(order);
    }

    private void SetCart() { var cartCount = 0;
      cartCount = _securityAdapter.GetCartCount();
      Session["cart"] = cartCount; }

    }
}