using AlbumApp.Business.Entities;
using AlbumApp.Business.Managers;
using AlbumApp.Data.Contracts;
using Autofac.Extras.Moq;
using AlbumApp.Core.Common.Contracts;
using System.Security.Principal;
using System.Threading;
using Xunit;

namespace AlbumApp.Managers.Tests
{
  public class CartManagerTests
  {
    public CartManagerTests()
    {
      // remove self from admin group and tests should fail
      // then setup a fake admin user and verify tests pass
      GenericPrincipal principal = new GenericPrincipal(
        new GenericIdentity("GenericUser"), new string[] { "AlbumAppAdmin" });
      Thread.CurrentPrincipal = principal;
    }

    //[Fact]
    //public void GetCartItemsByCreatedDate() {

    //}

    //[Fact]
    //public void UpdateAlbum_add_new() {
    //  using (var mock = AutoMock.GetLoose()) {
    //    Album newAlbum = new Album();
    //    Album addedAlbum = new Album { AlbumId = 1 };
    //    mock.Mock<IDataRepositoryFactory>().Setup(g =>
    //        g.GetDataRepository<IAlbumRepository>().Add(newAlbum)).Returns(addedAlbum);
    //    InventoryManager manager = mock.Create<InventoryManager>();

    //    Album ret = manager.UpdateAlbum(newAlbum);

    //    Assert.Equal(addedAlbum, ret); } }

    //[Fact]
    //public void UpdateAlbum_update_existing() {
    //  using (var mock = AutoMock.GetLoose()) {
    //    Album existingAlbum = new Album { AlbumId = 1 };
    //    Album updatedAlbum = new Album { AlbumId = 1 };
    //    mock.Mock<IDataRepositoryFactory>().Setup(g =>
    //        g.GetDataRepository<IAlbumRepository>().Update(existingAlbum)).Returns(updatedAlbum);
    //    InventoryManager manager = mock.Create<InventoryManager>();

    //    Album ret = manager.UpdateAlbum(existingAlbum);

    //    Assert.Equal(updatedAlbum, ret); } }


  }
}
