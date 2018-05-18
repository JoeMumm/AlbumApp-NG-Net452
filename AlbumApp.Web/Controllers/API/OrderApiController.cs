using AlbumApp.Client.Contracts;
using AlbumApp.Client.Entities;
using AlbumApp.Core.Common.Contracts;
using AlbumApp.Web.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace AlbumApp.Web.Controllers.API
{
  [RoutePrefix("api/order")]
  [UsesDisposableService]
  public class OrderApiController : ApiControllerBase
  {
    private readonly IOrderService _orderService;

    public OrderApiController(IOrderService orderService)
    { _orderService = orderService; }


    protected override void RegisterServices(List<IServiceContract> disposableServices)
    { disposableServices.Add(_orderService); }


    [HttpPost]
    [Route("create")] // /
    public IHttpActionResult Create(string order) { //
      var _order = JsonConvert.DeserializeObject<Order>(order);
      _order.Created = DateTime.Now; 
      return GetHttpResponse(Request, () => {
        var newOrder = _orderService.Add(_order);
        return Ok(newOrder.OrderId);  }); }

  }
}