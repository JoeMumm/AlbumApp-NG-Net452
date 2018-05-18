using AlbumApp.Client.Contracts;
using AlbumApp.Client.Entities;
using AlbumApp.Core.Common.Contracts;
using AlbumApp.Web.Core;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace AlbumApp.Web.Controllers
{
  [UsesDisposableService]
  [Authorize]
  public class CartController : ViewControllerBase
  {
    private readonly ICartService _cartService;
    private readonly ISecurityAdapter _securityAdapter;

    public CartController(ICartService cartService)
    { _cartService = cartService; }

    public CartController(ISecurityAdapter securityAdapter)
    { _securityAdapter = securityAdapter; }

    public CartController(ICartService cartService, ISecurityAdapter securityAdapter)
    { _cartService = cartService; _securityAdapter = securityAdapter; }


    protected override void RegisterServices(List<IServiceContract> disposableServices)
    { disposableServices.Add(_cartService); }


    [HttpGet]
    public ActionResult Index(bool checkout = false)
    {
      if (User != null) SetCart();
      ViewBag.Checkout = checkout;
      return View();
    }

    private void SetCart() {
      var cartCount = 0;
      cartCount = _securityAdapter.GetCartCount();
      Session["cart"] = cartCount; }

    //[HttpPost]
    public ActionResult Add(int albumId, decimal price) { 
      var accountId = _securityAdapter.GetAccountId();
      var cartItem = new CartItem { AccountId = accountId, AlbumId = albumId,
        OrderId = 1, Price = price, Quantity = 1, Created =DateTime.Now,};
      _cartService.Add(cartItem);
      
      return RedirectToAction("Index");
    }

  }
}