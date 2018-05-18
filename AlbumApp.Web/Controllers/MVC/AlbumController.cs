using AlbumApp.Client.Contracts;
using AlbumApp.Core.Common.Contracts;
using AlbumApp.Web.Core;
using AlbumApp.Web.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace AlbumApp.Web.Controllers.MVC
{
  [UsesDisposableService]
  public class AlbumController : ViewControllerBase
  {
    private readonly IInventoryService _inventoryService;

    public AlbumController(IInventoryService inventoryService)
    { _inventoryService = inventoryService; }

    protected override void RegisterServices(List<IServiceContract> disposableServices)
    { disposableServices.Add(_inventoryService); }

    [HttpGet]
    public ActionResult Index() {
      return View(); }

    [HttpGet]
    public ActionResult Details(int id) {
      var vM = new AlbumDetailsVM();
      var album = _inventoryService.GetAlbumTracksById(id);
      vM.AlbumId = album.AlbumId;  vM.Title = album.Title;
      vM.Genre = getGenre(album.Genre);  vM.Artist = album.Artist;
      vM.Price = album.Price; vM.Tracks = album.Tracks;
      vM.Image = album.Image;
      return View(vM); }

    private string getGenre(int genreNumber) { var genre = "";
    switch (genreNumber) {
      case 1: genre = "Afro-Cuban Jazz"; break;
      case 2: genre = "Brazilian Jazz"; break;
      case 3: genre = "Classical"; break;
      case 4: genre = "Jazz"; break;
      case 5: genre = "Rock and Roll"; break;
      default: genre = ""; break; }
    return genre; }

  }
}