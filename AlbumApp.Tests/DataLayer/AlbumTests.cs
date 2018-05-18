using AlbumApp.Business.Bootstrapper;
using AlbumApp.Business.Entities;
using AlbumApp.Core.Common.Contracts;
using AlbumApp.Core.Common.Core;
using AlbumApp.Data.Contracts;
using Autofac;
using Autofac.Extras.Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace AlbumApp.DataLayer.Tests
{
  public class AlbumTests
  {
    public AlbumTests()
    {
      var builder = AutoFacLoader.Builder();
      builder.Register(r => new AlbumRepositoryTestClass(r.Resolve<IAlbumRepository>()));
      builder.Register(r => new AlbumRepositoryFactoryTestClass(r.Resolve<IDataRepositoryFactory>()));
      ObjectBase.Container = builder.Build();
      _albums = new List<Album> {
        new Album() { AlbumId = 1, Title = "Title1", Artist = "Artist1", Genre  = 1, AlbumNumber = "AlNm1", Price = 1, StockAmount = 1 },
        new Album() { AlbumId = 2, Title = "Title2", Artist = "Artist2", Genre  = 2, AlbumNumber = "AlNm2", Price = 2, StockAmount = 2 } };
    }

    List<Album> _albums;

    AlbumRepositoryTestClass _albumRepositoryTestClass;

    [Fact]
    public void test_albumRepository_add_delete_dbusage() { // Integration Test, accesses database
      Album newAlbum = new Album { Title = "Title3", Artist = "Artist3", Genre = 3, AlbumNumber = "AlNm3", Price = 3, StockAmount = 3 };
      using (var container = ObjectBase.Container)
      {
        _albumRepositoryTestClass = container.Resolve<AlbumRepositoryTestClass>();
        int _alCnt = GetAlbumCount();
        Album createdAlbum = AddAlbum(newAlbum);
        Assert.Equal(_alCnt + 1, GetAlbumCount());
        newAlbum.AlbumId = createdAlbum.AlbumId;
        Assert.Equal(newAlbum, createdAlbum);
        DeleteAlbum(createdAlbum.AlbumId);
        Assert.Equal(_alCnt, GetAlbumCount());
        createdAlbum = AddAlbum(newAlbum);
        DeleteAlbum(createdAlbum);
        Assert.Equal(_alCnt, GetAlbumCount()); } }

    private int GetAlbumCount() {
      var albums = _albumRepositoryTestClass.GetAlbums();
      return albums.Count(); }

    private Album AddAlbum(Album newAlbum) {
        return _albumRepositoryTestClass.AddAlbum(newAlbum); }
  
    private void DeleteAlbum(int albumId) {
      _albumRepositoryTestClass.DeleteAlbum(albumId); }
  
    private void DeleteAlbum(Album album) {
      _albumRepositoryTestClass.DeleteAlbum(album); }


    [Fact]
    public void test_albumRepository_get_usage() { // Integration Test, accesses database
      using (var container = ObjectBase.Container) {
        var albums = container.Resolve<AlbumRepositoryTestClass>().GetAlbums();

        Assert.True(albums != null);
        Assert.NotEmpty(albums);
        Assert.True(albums.Count() > 0); } }

    [Fact]
    public void test_albumRepository_get_id_usage() { // Integration Test, accesses database
      using (var container = ObjectBase.Container) {
        var album = container.Resolve<AlbumRepositoryTestClass>().GetAlbum(1);

        Assert.True(album != null);
        Assert.True(album.AlbumId == 1); } }

    [Fact]
    public void test_albumRepository_get_byid_usage() { // Integration Test, accesses database
      int albumId = 1;
      using (var container = ObjectBase.Container) {
        var album = container.Resolve<AlbumRepositoryTestClass>().GetById(albumId);

        Assert.True(album != null);
        Assert.True(album.AlbumId == albumId); } }

    [Fact]
    public void test_albumRepository_get_mocking() {
      using (var mock = AutoMock.GetLoose()) {
        mock.Mock<IAlbumRepository>().Setup(g => g.Get()).Returns(_albums);
        var repositoryTestClass = mock.Create<AlbumRepositoryTestClass>();

        var ret = repositoryTestClass.GetAlbums();

        Assert.Equal(_albums, ret); } }

    [Fact]
    public void test_albumRepository_get_id_mocking() {
      using (var mock = AutoMock.GetLoose()) { int albumId = 1;
        mock.Mock<IAlbumRepository>().Setup(g => g.Get(albumId)).Returns(_albums.First());
        var repositoryTestClass = mock.Create<AlbumRepositoryTestClass>();

        var ret = repositoryTestClass.GetAlbum(albumId);

        Assert.Equal(_albums.First(), ret); } }

    [Fact]
    public void test_albumRepository_get_byid_mocking() {
      using (var mock = AutoMock.GetLoose()) { int albumId = 1;
        mock.Mock<IAlbumRepository>().Setup(g => g.GetById(albumId)).Returns(_albums.First());
        var repositoryTestClass = mock.Create<AlbumRepositoryTestClass>();

        var ret = repositoryTestClass.GetById(albumId);

        Assert.Equal(_albums.First(), ret); } }

    [Fact]
    public void test_albumRepositoryFactory_usage() { // Integration Test, accesses database
      using (var container = ObjectBase.Container) {  
        var albums = container.Resolve<AlbumRepositoryFactoryTestClass>().GetAlbums();

        Assert.True(albums != null);
        Assert.NotEmpty(albums);
        Assert.True(albums.Count() > 0); } }

    [Fact]
    public void test_albumRepositoryFactory_byID_usage() { // Integration Test, accesses database
      using (var container = ObjectBase.Container) { ;
        Album album = container.Resolve<AlbumRepositoryFactoryTestClass>().GetAlbumsById(1);

        Assert.True(album != null); }}


    [Fact]
    public void test_albumRepositoryFactory_mocking1()
    {
      using (var mock = AutoMock.GetLoose())
      {
        mock.Mock<IDataRepositoryFactory>().Setup(g => g.GetDataRepository<IAlbumRepository>().Get()).Returns(_albums);
        var repositoryTestClass = mock.Create<AlbumRepositoryFactoryTestClass>();

        IEnumerable<Album> ret = repositoryTestClass.GetAlbums();

        Assert.Equal(_albums, ret); } }

  }

  public class AlbumRepositoryTestClass {
    IAlbumRepository _albumRepository;
    public AlbumRepositoryTestClass(IAlbumRepository albumRepository)
    { _albumRepository = albumRepository; }
    
    public Album AddAlbum(Album newAlbum) {
      Album createdAlbum = _albumRepository.Add(newAlbum);
      return createdAlbum; }

    public IEnumerable<Album> GetAlbums() {
      IEnumerable<Album> albums = _albumRepository.Get();
      return albums;  }
    
    public Album GetAlbum(int albumId) {
        Album album = _albumRepository.Get(albumId);
        return album;  }
    
    public Album UpdateAlbum(Album album) {
        Album modifiedAlbum = _albumRepository.Update(album);
        return modifiedAlbum;  }
    
     public void DeleteAlbum(int albumId) { _albumRepository.Remove(albumId); }
    
     public void DeleteAlbum(Album album) { _albumRepository.Remove(album); }
    
    public Album GetById(int albumId) {
        Album album = _albumRepository.GetById(albumId);
        return album; }
  }

  public class AlbumRepositoryFactoryTestClass {
    public AlbumRepositoryFactoryTestClass(IDataRepositoryFactory dataRepositoryFactory)
    { _dataRepositoryFactory = dataRepositoryFactory; }
      
      IDataRepositoryFactory _dataRepositoryFactory;

      public IEnumerable<Album> GetAlbums() {
        IAlbumRepository albumRepository =
          _dataRepositoryFactory.GetDataRepository<IAlbumRepository>();

      IEnumerable<Album> albums = albumRepository.Get();
      return albums; }
    
      public Album GetAlbumsById(int albumId) {
        IAlbumRepository albumRepository =
          _dataRepositoryFactory.GetDataRepository<IAlbumRepository>();

        Album album = albumRepository.GetById(albumId);
        return album; }
  }
}
