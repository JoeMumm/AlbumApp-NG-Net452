using AlbumApp.Web.Controllers.MVC;
using AlbumApp.Web.Core;
using AlbumApp.Web.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Xunit;

namespace AlbumApp.Web.Controllers.Tests
{
  public class AccountControllerTests
  {
    [Fact]
    public void Login() {
      // Arrange
      Mock<ISecurityAdapter> mockSecurityAdapter = new Mock<ISecurityAdapter>();
      string returnUrl = "/testreturnurl";

      // Act
      AccountController controller = new AccountController(mockSecurityAdapter.Object);
      ActionResult result = controller.Login(returnUrl);

      // Assert
      Assert.True(result is ViewResult);
      ViewResult viewResult = result as ViewResult;
      Assert.True(viewResult.Model is AccountLoginModel);
      AccountLoginModel model = viewResult.Model as AccountLoginModel;
      Assert.True(model.ReturnUrl == returnUrl); }



  }
}
