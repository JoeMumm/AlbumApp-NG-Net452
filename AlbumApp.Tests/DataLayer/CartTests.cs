using Autofac;
using Autofac.Extras.Moq;
using AlbumApp.Business.Bootstrapper;
using AlbumApp.Core.Common.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using AlbumApp.Data.Contracts;
using AlbumApp.Business.Entities;
using AlbumApp.Core.Common.Contracts;

namespace AlbumApp.DataLayer.Tests
{
  public class CartTests
  {
    public CartTests() {
      var builder = AutoFacLoader.Builder();
      builder.Register(r => new CartRepositoryFactoryTestClass(r.Resolve<IDataRepositoryFactory>()));
      ObjectBase.Container = builder.Build();
      //_albums = new List<Album> {
      //  new Album() { AlbumId = 1, Title = "Title1", Artist = "Artist1", Genre  ="Genre1", AlbumNumber = "AlNm1", Price = 1, StockAmount = 1 },
      //  new Album() { AlbumId = 2, Title = "Title2", Artist = "Artist2", Genre  ="Genre2", AlbumNumber = "AlNm2", Price = 2, StockAmount = 2 } };
    }

    //List<Album> _albums;

    //[Fact] //(Skip = "skip")
    //public void test_albumRepository_add_delete_dbusage() { // Integration Test, accesses database
    //  int _alCnt = GetAlbumCount();
    //  Album newAlbum = new Album { Title = "Title3", Artist = "Artist3", Genre = "Genre3", AlbumNumber = "AlNm3", Price = 3, StockAmount = 3 };
    //  Album createdAlbum = AddAlbum(newAlbum);
    //  Assert.Equal(_alCnt + 1, GetAlbumCount());
    //  newAlbum.AlbumId = createdAlbum.AlbumId;
    //  Assert.Equal(newAlbum, createdAlbum);
    //  DeleteAlbum(createdAlbum.AlbumId);
    //  Assert.Equal(_alCnt, GetAlbumCount());
    //  createdAlbum = AddAlbum(newAlbum);
    //  DeleteAlbum(createdAlbum);
    //  Assert.Equal(_alCnt, GetAlbumCount()); }

    //private int GetAlbumCount() {
    //  using (var container = ContainerSetup()) {
    //    var albums = container.Resolve<AlbumRepositoryTestClass>().GetAlbums();
    //    return albums.Count(); } }

    //private Album AddAlbum(Album newAlbum) {
    //  using (var container = ContainerSetup()) {
    //    return container.Resolve<AlbumRepositoryTestClass>().AddAlbum(newAlbum); } }

    //private void DeleteAlbum(int albumId) {
    //  using (var container = ContainerSetup()) {
    //    container.Resolve<AlbumRepositoryTestClass>().DeleteAlbum(albumId); } }

    //private void DeleteAlbum(Album album) {
    //  using (var container = ContainerSetup()) {
    //    container.Resolve<AlbumRepositoryTestClass>().DeleteAlbum(album); } }

    //private IContainer ContainerSetup() {
    //  ContainerBuilder builder = AutoFacLoader.Init();
    //  builder.Register(r => new AlbumRepositoryTestClass(r.Resolve<IAlbumRepository>()));
    //  return builder.Build(); }

    //[Fact]
    //public void test_albumRepository_get_usage()
    //{ // Integration Test, accesses database
    //  ContainerBuilder builder = ObjectBase.ContainerBuilder;
    //  builder.Register(r => new AlbumRepositoryTestClass(r.Resolve<IAlbumRepository>()));
    //  ObjectBase.Container = builder.Build();

    //  using (var container = ObjectBase.Container) {
    //    var albums = container.Resolve<AlbumRepositoryTestClass>().GetAlbums();

    //    Assert.True(albums != null);
    //    Assert.NotEmpty(albums);
    //    Assert.True(albums.Count() > 0); } }

    //[Fact]
    //public void test_albumRepository_get_id_usage() { // Integration Test, accesses database
    //  ContainerBuilder builder = ObjectBase.ContainerBuilder;
    //  builder.Register(r => new AlbumRepositoryTestClass(r.Resolve<IAlbumRepository>()));
    //  ObjectBase.Container = builder.Build();

    //  using (var container = ObjectBase.Container) {
    //    var album = container.Resolve<AlbumRepositoryTestClass>().GetAlbum(1);

    //    Assert.True(album != null);
    //    Assert.True(album.AlbumId == 1); } }

    //[Fact]
    //public void test_albumRepository_get_byid_usage() { // Integration Test, accesses database
    //  ContainerBuilder builder = ObjectBase.ContainerBuilder; int albumId = 1;
    //  builder.Register(r => new AlbumRepositoryTestClass(r.Resolve<IAlbumRepository>()));
    //  ObjectBase.Container = builder.Build();

    //  using (var container = ObjectBase.Container) {
    //    var album = container.Resolve<AlbumRepositoryTestClass>().GetById(albumId);

    //    Assert.True(album != null);
    //    Assert.True(album.AlbumId == albumId); } }

    //[Fact]
    //public void test_albumRepository_get_mocking() {
    //  using (var mock = AutoMock.GetLoose()) {
    //    mock.Mock<IAlbumRepository>().Setup(g => g.Get()).Returns(_albums);
    //    var repositoryTestClass = mock.Create<AlbumRepositoryTestClass>();

    //    var ret = repositoryTestClass.GetAlbums();

    //    Assert.Equal(_albums, ret); } }

    //[Fact]
    //public void test_albumRepository_get_id_mocking() {
    //  using (var mock = AutoMock.GetLoose()) { int albumId = 1;
    //    mock.Mock<IAlbumRepository>().Setup(g => g.Get(albumId)).Returns(_albums.First());
    //    var repositoryTestClass = mock.Create<AlbumRepositoryTestClass>();

    //    var ret = repositoryTestClass.GetAlbum(albumId);

    //    Assert.Equal(_albums.First(), ret); } }

    //[Fact]
    //public void test_albumRepository_get_byid_mocking() {
    //  using (var mock = AutoMock.GetLoose()) { int albumId = 1;
    //    mock.Mock<IAlbumRepository>().Setup(g => g.GetById(albumId)).Returns(_albums.First());
    //    var repositoryTestClass = mock.Create<AlbumRepositoryTestClass>();

    //    var ret = repositoryTestClass.GetById(albumId);

    //    Assert.Equal(_albums.First(), ret); } }

    [Fact]
    public void test_cartRepositoryFactory_usage() { // Integration Test, accesses database
      using (var container = ObjectBase.Container) { 
        var cartItems =
          container.Resolve<CartRepositoryFactoryTestClass>().GetCartItems();

        Assert.True(cartItems != null);
        Assert.NotEmpty(cartItems);
        Assert.True(cartItems.Count() > 0); } }

    [Fact]
    public void test_cartRepositoryFactory_byid_usage() { // Integration Test, accesses database
      using (var container = ObjectBase.Container) { 
        var cartItems =
          container.Resolve<CartRepositoryFactoryTestClass>().GetCartById(1);

        Assert.True(cartItems != null);
        //Assert.NotEmpty(cartItems);
        //Assert.True(cartItems.Count() > 0);
      } }


    [Fact] 
    public void test_cartRepositoryFactory_byDate_usage() { // Integration Test, accesses database
      using (var container = ObjectBase.Container) { 
        var cartItems =
          container.Resolve<CartRepositoryFactoryTestClass>().GetCartItemsCreationDate(DateTime.Now);

        Assert.True(cartItems != null);
        Assert.NotEmpty(cartItems);
        Assert.True(cartItems.Count() > 0); } }
    
    [Fact]
    public void test_cartRepositoryFactory_getall_usage() { // Integration Test, accesses database
      using (var container = ObjectBase.Container) { 
        var cartItems =
          container.Resolve<CartRepositoryFactoryTestClass>().GetAllCartItems();

        Assert.True(cartItems != null);
        Assert.NotEmpty(cartItems);
        Assert.True(cartItems.Count() > 0); } }


    //[Fact]
    //public void test_albumRepositoryFactory_mocking1()
    //{
    //  using (var mock = AutoMock.GetLoose())
    //  {
    //    mock.Mock<IDataRepositoryFactory>().Setup(g => g.GetDataRepository<IAlbumRepository>().Get()).Returns(_albums);
    //    var repositoryTestClass = mock.Create<AlbumRepositoryFactoryTestClass>();

    //    IEnumerable<Album> ret = repositoryTestClass.GetAlbums();

    //    //mock.Mock<IDataRepositoryFactory>().Verify(g => g.GetDataRepository<IAlbumRepository>());


    //    //Album[] albums1 = new Album[] { new Album { AlbumId = 1 }, new Album { AlbumId = 2 }, new Album { AlbumId = 3 } };
    //    //List<Album> albums2 = new List<Album> { new Album { AlbumId = 1 }, new Album { AlbumId = 2 }, new Album { AlbumId = 3 } };

    //    //Assert.True(albums1.SequenceEqual(albums1));
    //    //Assert.Equal(albums1, albums2);


    //    Assert.Equal(_albums, ret);
    //  }
    //}

    //[Fact]
    //public void test_albumRepositoryFactory_mocking2() {
    //  using (var mock = AutoMock.GetLoose()) {
    //    mock.Mock<IAlbumRepository> mockAlbumRepository = new Mock<IAlbumRepository>(); // Autofac.Extras.Moq doesn't appear to allow
    //    mock.Mock<IAlbumRepository>().Setup(g => g.Get()).Returns(_albums);
    //    mock.Mock<IDataRepositoryFactory>().Setup(g => g.GetDataRepository<IAlbumRepository>()).Returns(IAlbumRepository.Get);
    //    var repositoryTestClass = mock.Create<AlbumRepositoryFactoryTestClass>();

    //    var ret = repositoryTestClass.GetAlbums();

    //    mock.Mock<IDataRepositoryFactory>().Verify(g => g.GetDataRepository<IAlbumRepository>());
    //    Assert.Equal(_albums, ret); } }
  }

  //public class AlbumRepositoryTestClass {
  //  IAlbumRepository _albumRepository;
  //  public AlbumRepositoryTestClass(IAlbumRepository albumRepository)
  //  { _albumRepository = albumRepository; }

  //  public Album AddAlbum(Album newAlbum) {
  //    using (ObjectBase.Container) {
  //      Album createdAlbum = _albumRepository.Add(newAlbum);
  //      return createdAlbum; } }

  //  public IEnumerable<Album> GetAlbums() {
  //    using (ObjectBase.Container) {
  //      IEnumerable<Album> albums = _albumRepository.Get();
  //      return albums; } }

  //  public Album GetAlbum(int albumId) {
  //    using (ObjectBase.Container) {
  //      Album album = _albumRepository.Get(albumId);
  //      return album; } }

  //  public Album UpdateAlbum(Album album) {
  //    using (ObjectBase.Container) {
  //      Album modifiedAlbum = _albumRepository.Update(album);
  //      return modifiedAlbum; } }

  //   public void DeleteAlbum(int albumId) {
  //    using (ObjectBase.Container) { _albumRepository.Remove(albumId); } }

  //   public void DeleteAlbum(Album album) {
  //    using (ObjectBase.Container) { _albumRepository.Remove(album); } }

  //  // Custom method testing
  //  public Album GetById(int albumId) {
  //    using (ObjectBase.Container) {
  //      Album album = _albumRepository.GetById(albumId);
  //      return album;
  //    } } }

  public class CartRepositoryFactoryTestClass {
    IDataRepositoryFactory _dataRepositoryFactory;
    public CartRepositoryFactoryTestClass(IDataRepositoryFactory dataRepositoryFactory)
    { _dataRepositoryFactory = dataRepositoryFactory; }
    
      public CartItem GetCartById(int cartItemId) {
        ICartRepository cartRepository =
          _dataRepositoryFactory.GetDataRepository<ICartRepository>();

      //using (ObjectBase.Container) { }
        CartItem item = cartRepository.GetByID(cartItemId);
        return item; }


    public IEnumerable<CartItem> GetCartItems() {
      ICartRepository cartRepository =
        _dataRepositoryFactory.GetDataRepository<ICartRepository>();

      IEnumerable<CartItem> cartItems = cartRepository.Get();
      return cartItems; }

    public IEnumerable<CartItem> GetCartItemsCreationDate(DateTime date) {
      ICartRepository cartRepository =
        _dataRepositoryFactory.GetDataRepository<ICartRepository>();

      IEnumerable<CartItem> cartItems = cartRepository.GetCartItemsByCreatedDate(date);
      return cartItems; } 
  
    public IEnumerable<CartItem> GetAllCartItems() {
      ICartRepository cartRepository =
        _dataRepositoryFactory.GetDataRepository<ICartRepository>();

      IEnumerable<CartItem> cartItems = cartRepository.GetAllCartItems();
      return cartItems; }

  }
}
