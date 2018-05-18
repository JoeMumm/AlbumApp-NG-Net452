using System.Web.Mvc;
using Xunit;
using Moq;
using AlbumApp.Web.Core;

namespace AlbumApp.Web.Controllers.Tests
{
  public class HomeControllerTest
  {
    [Fact]
    public void Index()
    {
      // Arrange
     Mock<ISecurityAdapter> mockSecurityAdapter = new Mock<ISecurityAdapter>();
     HomeController controller = new HomeController(mockSecurityAdapter.Object);

      // Act
      ViewResult result = controller.Index() as ViewResult;

      // Assert
      Assert.NotNull(result);
      Assert.Equal("Home Page", result.ViewBag.Title);
    }
  }
}
