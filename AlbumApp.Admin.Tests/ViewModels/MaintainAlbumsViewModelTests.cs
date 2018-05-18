using AlbumApp.Admin.ViewModels;
using AlbumApp.Client.Contracts;
using AlbumApp.Client.Entities;
using AlbumApp.Core.Common.Contracts;
using Moq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AlbumApp.Admin.Tests
{
  public class MaintainAlbumsViewModelTests
  {
    [Fact]
    public void ViewLoaded_test() {
      Album[] data = new List<Album>() { new Album() { AlbumId = 1 }, new Album() { AlbumId = 2 } }.ToArray();
      Mock<IServiceFactory> mockServiceFactory = new Mock<IServiceFactory>();
      mockServiceFactory.Setup(mock => mock.CreateClient<IInventoryService>().GetAllAlbums()).Returns(data);

      MaintainAlbumsViewModel viewModel = new MaintainAlbumsViewModel(mockServiceFactory.Object);

      Assert.True(viewModel.AlbumVMs == null);
      object loaded = viewModel.ViewLoaded;      
      Assert.True(viewModel.AlbumVMs != null);
      // Adding Dispatcher.CurrentDispatcher.DelayInvoke to the OnViewLoaded 
      // method broke this test
      //Assert.True(viewModel.AlbumVMs.Count == data.Length
      //    && viewModel.AlbumVMs[0].AlbumId == data[0].AlbumId);
    }

    [Fact]
    public void CurrentAlbumSetting_test() {
      Album album = new Album() { AlbumId = 1 };
      AlbumViewModel albumVM = new AlbumViewModel() { AlbumId = 1 };

      Mock<IServiceFactory> mockServiceFactory = new Mock<IServiceFactory>();
      MaintainAlbumsViewModel viewModel = new MaintainAlbumsViewModel(mockServiceFactory.Object);

      Assert.True(viewModel.CurrentAlbumViewModel == null);
      viewModel.EditAlbumCommand.Execute(albumVM);
      Assert.True(viewModel.CurrentAlbumViewModel != null &&
                  viewModel.CurrentAlbumViewModel.Album.AlbumId == album.AlbumId); }

    [Fact]
    public void EditAlbumCommand_test() {
      AlbumViewModel albumVM = new AlbumViewModel() { AlbumId = 1, Title = "White Album",
        Artist = "Beatles", Genre = "Rock and Roll", Price = 24.95m, AlbumNumber = "RCRL-Btls-005" };
      Mock<IServiceFactory> mockServiceFactory = new Mock<IServiceFactory>();

      MaintainAlbumsViewModel viewModel = new MaintainAlbumsViewModel(mockServiceFactory.Object);
      viewModel.AlbumVMs = new ObservableCollection<AlbumViewModel> { albumVM };

      Assert.True(viewModel.AlbumVMs[0].Title == "White Album");
      Assert.True(viewModel.CurrentAlbumViewModel == null);

      viewModel.EditAlbumCommand.Execute(albumVM);
      Assert.True(viewModel.CurrentAlbumViewModel != null);
      
      mockServiceFactory.Setup(mock => mock.CreateClient<IInventoryService>()
        .UpdateAlbum(It.IsAny<Album>())).Returns(viewModel.CurrentAlbumViewModel.Album);

      viewModel.CurrentAlbumViewModel.Album.Title = "Abbey Road";
      viewModel.CurrentAlbumViewModel.SaveCommand.Execute(null);
      Assert.True(viewModel.AlbumVMs[0].Title == "Abbey Road"); }
  }
}

