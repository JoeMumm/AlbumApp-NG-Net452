using AlbumApp.Business.Entities;
using AlbumApp.Data.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace AlbumApp.Data
{
  public class TrackRepository : DataRepositoryBase<Track>, ITrackRepository
  {
    protected override Track AddEntity(AlbumContext entityContext, Track entity) {
      return entityContext.TrackSet.Add(entity); } 

    protected override Track UpdateEntity(AlbumContext entityContext, Track entity) {
      return (from e in entityContext.TrackSet
              where e.TrackId == entity.TrackId
              select e).FirstOrDefault(); }

    protected override IEnumerable<Track> GetEntities(AlbumContext entityContext)
    { return from e in entityContext.TrackSet select e; }

    protected override Track GetEntity(AlbumContext entityContext, int id) {
      var query = (from e in entityContext.TrackSet where e.TrackId == id select e);
      var results = query.FirstOrDefault();
      return results; }
    
  }
}
