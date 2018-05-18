using AlbumApp.Client.Contracts;
using AlbumApp.Client.Entities;
using AlbumApp.Core.Common.Contracts;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AlbumApp.Admin.ViewModels.Tests
{
  public class EditAlbumViewModelTests
  {
    [Fact]
    public void ViewModelConstruction_test() {
      Album album = new Album() { AlbumId = 1, Title = "White Album" };
      Mock<IServiceFactory> mockServiceFactory = new Mock<IServiceFactory>();

      EditAlbumViewModel viewModel = new EditAlbumViewModel(mockServiceFactory.Object, album);

      Assert.True(viewModel.Album != null && viewModel.Album != album);
      Assert.True(viewModel.Album.AlbumId == album.AlbumId && viewModel.Album.Title == album.Title); }

    [Fact]
    public void SaveCommand_test() {
      Album album = new Album() { AlbumId = 1, Title = "White Album", Artist = "Beatles",
         Genre = 5, AlbumNumber = "RCRL-Btls-034", Price = 14.90M };
      Mock<IServiceFactory> mockServiceFactory = new Mock<IServiceFactory>();

      EditAlbumViewModel viewModel = new EditAlbumViewModel(mockServiceFactory.Object, album);
      mockServiceFactory.Setup(mock => mock.CreateClient<IInventoryService>()
                        .UpdateAlbum(It.IsAny<Album>())).Returns(viewModel.Album);
      viewModel.Album.Title = "Revolver";
      bool albumUpdated = false;          string title = string.Empty;
      viewModel.AlbumUpdated += (s, e) => {
        albumUpdated = true; title = e.Album.Title; };
      viewModel.SaveCommand.Execute(null);

      Assert.True(albumUpdated);
      Assert.True(title == "Revolver"); }

    [Fact]
    public void CanSaveCommand_test() {
      Album album = new Album() { AlbumId = 1, Title = "White Album", Artist = "Beatles",
         Genre = 5, AlbumNumber = "RCRL-Btls-034", Price = 14.90M };
      Mock<IServiceFactory> mockServiceFactory = new Mock<IServiceFactory>();

      EditAlbumViewModel viewModel = new EditAlbumViewModel(mockServiceFactory.Object, album);

      Assert.False(viewModel.SaveCommand.CanExecute(null));
      viewModel.Album.Title = "Revolver";
      Assert.True(viewModel.SaveCommand.CanExecute(null)); }

    [Fact]
    public void AlbumIsValid_isValid_test() {
      Album album = new Album() { AlbumId = 1, Title = "White Album", Artist = "Beatles",
         Genre = 5, AlbumNumber = "RCRL-Btls-034", Price = -1m };
      Mock<IServiceFactory> mockServiceFactory = new Mock<IServiceFactory>();

      EditAlbumViewModel viewModel = new EditAlbumViewModel(mockServiceFactory.Object, album);

      Assert.True(!viewModel.Album.IsValid);
      viewModel.Album.Price = 17.5m;
      Assert.True(viewModel.Album.IsValid); }

    [Fact]
    public void CancelCommand_test() {
      Album album = new Album() { AlbumId = 1, Title = "White Album" };
      Mock<IServiceFactory> mockServiceFactory = new Mock<IServiceFactory>();

      EditAlbumViewModel viewModel = new EditAlbumViewModel(mockServiceFactory.Object, album);
      bool canceled = false;
      viewModel.CancelEditAlbum += (s, e) => canceled = true;

      Assert.True(!canceled);
      viewModel.CancelCommand.Execute(null);
      Assert.True(viewModel.CancelCommand.CanExecute(null));
      Assert.True(canceled); }

  }
}
