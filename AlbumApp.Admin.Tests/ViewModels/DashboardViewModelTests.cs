using System;
using System.Collections.Generic;
using System.Linq;
using AlbumApp.Admin.ViewModels;
using AlbumApp.Client.Contracts;
using AlbumApp.Client.Entities;
using AlbumApp.Core.Common.Contracts;
using Moq;
using Xunit;

namespace AlbumApp.Admin.Tests
{
  public class DashboardViewModelTests
  {
    [Fact]
    public void ViewLoaded_test()
    {
      Album[] data = new List<Album>() {
                new Album(), new Album() }.ToArray();

      Mock<IServiceFactory> mockServiceFactory = new Mock<IServiceFactory>();
      mockServiceFactory.Setup(mock => mock.CreateClient<IInventoryService>().GetAllAlbums()).Returns(data);

      DashboardViewModel viewModel = new DashboardViewModel(mockServiceFactory.Object);

      Assert.True(viewModel != null);

      Assert.True(viewModel.ViewTitle == "Home");
    }
  }
}
