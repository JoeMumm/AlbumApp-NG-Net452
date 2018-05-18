using AlbumApp.Client.Contracts;
using AlbumApp.Core.Common.Contracts;
using AlbumApp.Web.Core;
using Core.Common.Core;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Web.Http;

namespace AlbumApp.Web.Controllers.API
{
  [RoutePrefix("api/album")]
  [UsesDisposableService]
  public class AlbumApiController : ApiControllerBase
  {
    private readonly IInventoryService _inventoryService;

    public AlbumApiController(IInventoryService inventoryService)
    { _inventoryService = inventoryService; }

    protected override void RegisterServices(List<IServiceContract> disposableServices)
    { disposableServices.Add(_inventoryService); }

    [HttpGet]
    [Route("getallalbums")]
    public IHttpActionResult GetAllAlbums() {
      return GetHttpResponse(Request, () => {
        var albums = _inventoryService.GetAllAlbums();
        return Ok(albums); }); }


    [HttpGet]
    [Route("getallalbumspaged/{pageIndex}/{pageSize}")]
    public IHttpActionResult GetAllAlbumsPaged(string filters, string sorts,
          int pageIndex, int pageSize) { 
      var _filters = JsonConvert.DeserializeObject<AlbumFilter>(filters);
      var _sorts = JsonConvert.DeserializeObject<List<SortItem>>(sorts);

      return GetHttpResponse(Request, () => {
        int totalCount;
        var albums = _inventoryService.GetAllAlbumsPaged(out totalCount, _filters,
          _sorts, pageIndex, pageSize);
        var albums_Count = new { albums, totalCount };
        return Ok(albums_Count); });
     }
  }
}
