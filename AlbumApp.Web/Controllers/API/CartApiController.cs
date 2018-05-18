using AlbumApp.Client.Contracts;
using AlbumApp.Core.Common.Contracts;
using AlbumApp.Web.Core;
using System.Collections.Generic;
using System.Web;
using System.Web.Http;

namespace AlbumApp.Web.Controllers.API
{
  [RoutePrefix("api/cart")]
  [UsesDisposableService]
  public class CartApiController : ApiControllerBase
  {
    private readonly ICartService _cartService;

    public CartApiController(ICartService cartService)
    { _cartService = cartService; }

    protected override void RegisterServices(List<IServiceContract> disposableServices)
    { disposableServices.Add(_cartService); }

    [HttpGet]
    [Route("getpendingcartitemsbyaccountid/{accountId}")] // /
    public IHttpActionResult GetPendingCartItemsByAccountId(int accountId) {
      return GetHttpResponse(Request, () => {
        var cartItems = _cartService.GetPendingCartItemsByAccountId(accountId);
        return Ok(cartItems); }); }
    
    [HttpPost]
    [Route("updatecartitem/{cartItemId}/{quantity}")] // /
    public IHttpActionResult UpdateCartItem(int cartItemId, int quantity)
    {
      return GetHttpResponse(Request, () => {
        _cartService.UpdateCartItem(cartItemId, quantity);
        return Ok(); });
    }

    [HttpPost]
    [Route("deletecartitem/{cartItemId}")]
    public IHttpActionResult DeleteCartItem(int cartItemId) {
      return GetHttpResponse(Request, () => {
        _cartService.DeleteCartItem(cartItemId);
        return Ok(); }); }

    [HttpPost]
    [Route("transfercartitemtoorder/{orderId}/{accountId}")]
    public IHttpActionResult TransferCartItemsToOrder(int orderId, int accountId)
    {
      return GetHttpResponse(Request, () => {
        _cartService.TransferCartItemsToOrder(orderId, accountId);
        return Ok();
      });
    }

  }
}