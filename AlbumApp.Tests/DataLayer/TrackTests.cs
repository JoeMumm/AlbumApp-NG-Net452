using Autofac;
using Autofac.Extras.Moq;
using AlbumApp.Business.Bootstrapper;
using AlbumApp.Core.Common.Core;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using AlbumApp.Data.Contracts;
using AlbumApp.Business.Entities;

namespace AlbumApp.DataLayer.Tests
{
  public class TrackTests
  {
    public TrackTests()
    {
      var builder = AutoFacLoader.Builder();
      builder.Register(r => new TrackRepositoryTestClass(r.Resolve<ITrackRepository>()));
      ObjectBase.Container = builder.Build(); //  AutoFacLoader.Init()
      _tracks = new List<Track>
      { new Track() { TrackId = 1, Name = "Name1" },
        new Track() { TrackId = 2, Name = "Name2" } }; }

    List<Track> _tracks;
  
    [Fact]
    public void test_trackRepository_usage() { // Integration Test, accesses database
      using (var container = ObjectBase.Container) { 
        var tracks = container.Resolve<TrackRepositoryTestClass>().GetTracks();

        Assert.True(tracks != null);
        Assert.NotEmpty(tracks);
        Assert.True(tracks.Count() > 0); } }

    [Fact]
    public void test_trackRepository_mocking() {
      using (var mock = AutoMock.GetLoose()) {
        mock.Mock<ITrackRepository>().Setup(g => g.Get()).Returns(_tracks);
        var repositoryTestClass = mock.Create<TrackRepositoryTestClass>();

        var ret = repositoryTestClass.GetTracks();

        Assert.Equal(_tracks, ret); } } }


  public class TrackRepositoryTestClass {
    ITrackRepository _trackRepository;
    public TrackRepositoryTestClass(ITrackRepository trackRepository)
    { _trackRepository = trackRepository; }

    public IEnumerable<Track> GetTracks() {
      using (ObjectBase.Container) {
        IEnumerable<Track> tracks = _trackRepository.Get();
        return tracks; } } }
  
}
