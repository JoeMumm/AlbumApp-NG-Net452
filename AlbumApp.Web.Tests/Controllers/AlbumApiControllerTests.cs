using AlbumApp.Client.Contracts;
using AlbumApp.Client.Entities;
using AlbumApp.Web.Controllers.API;
using Core.Common.Core;
using Moq;
using System.Collections.Generic;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Http.Results;
using Xunit;

namespace AlbumApp.Web.Controllers.Tests
{
  public class AlbumApiControllerTests
  {
    [Fact]
    public void GetAllAlbums_ok_returnsdata_test() {
      Mock<IInventoryService> mockInventoryService
                          = new Mock<IInventoryService>();
      Album[] albums = { new Album { AlbumId = 1 },
        new Album { AlbumId = 2 } };
      mockInventoryService.Setup(obj => obj.GetAllAlbums()).Returns(albums);

      AlbumApiController controller = new AlbumApiController(mockInventoryService.Object);
      IHttpActionResult response = controller.GetAllAlbums();
      var contentResult = response as OkNegotiatedContentResult<Album[]>;

      Assert.NotNull(contentResult);
      Assert.NotNull(contentResult.Content);
      var data = contentResult.Content;
      Assert.Equal(data, albums); }

    [Fact]
    public void GetAllAlbumsPaged_test() {
      Mock<IInventoryService> mockInventoryService
                          = new Mock<IInventoryService>();
      Album[] albums = new Album[20];
        for (int i = 0; i < 20; i++) albums[i] = new Album { AlbumId = i + 1 };
      var albumFilter = new AlbumFilter { AlbumNumber = "", Artist = "",
          Title = "", Genre = 0, Price = 0, StockAmount = 0 };
      var sortItems = new List<SortItem>(); 
      int totalCount = 20;
      mockInventoryService.Setup(obj => obj.GetAllAlbumsPaged(out totalCount, It.IsAny<AlbumFilter>(),
            sortItems, 0, 4)).Returns(albums);

      AlbumApiController controller = new AlbumApiController(mockInventoryService.Object);
      dynamic response = controller.GetAllAlbumsPaged(Json.Encode(albumFilter),
            Json.Encode(sortItems), 0, 4);
      dynamic content = response.Content;

      Assert.NotNull(content);
      var data = content.albums;
      Assert.Equal(data, albums);
      var ttlCount = content.totalCount;
      Assert.Equal(totalCount, ttlCount); }

  }  
}
