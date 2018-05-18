using AlbumApp.Business.Contracts;
using AlbumApp.Business.Entities;
using AlbumApp.Core.Common.Contracts;
using AlbumApp.Core.Common.Extensions;
using AlbumApp.Data.Contracts;
using Core.Common.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;

namespace AlbumApp.Data
{
  public class AlbumRepository : DataRepositoryBase<Album>, IAlbumRepository
  {
    protected override Album AddEntity(AlbumContext entityContext, Album entity) {
      return entityContext.AlbumSet.Add(entity); } 

    protected override Album UpdateEntity(AlbumContext entityContext, Album entity) {
      return (from e in entityContext.AlbumSet where e.AlbumId == entity.AlbumId
                    select e).FirstOrDefault(); }

    protected override IEnumerable<Album> GetEntities(AlbumContext entityContext)
    { return from e in entityContext.AlbumSet select e; }

    protected override Album GetEntity(AlbumContext entityContext, int id) {
      var query = (from e in entityContext.AlbumSet where e.AlbumId == id select e);
      var results = query.FirstOrDefault();
      return results; }

    public Album GetById(int albumId) {
      using (AlbumContext entityContext = new AlbumContext()) {
        var query = from a in entityContext.AlbumSet
                    where a.AlbumId == albumId  select a;
        return query.FirstOrDefault(); } }

    public Album GetAlbumTracksById(int albumId) {
      using (AlbumContext entityContext = new AlbumContext()) {
        var query = from a in entityContext.AlbumSet.Include("Tracks")
                    where a.AlbumId == albumId select a;
        return query.FirstOrDefault(); } }

    public IEnumerable<Album> GetAllAlbumsPaged(out int totalCount, AlbumFilter filters,
          List<SortItem> sorts, int pageIndex = 0, int pageSize = 15) { 
      using (AlbumContext entityContext = new AlbumContext()) {
        if (sorts.Count == 0) sorts.Add(
             new SortItem { Column = "AlbumId", Direction = SortDirection.Ascending });
        IQueryable<Album> query;
        query = entityContext.AlbumSet;
        query = Filter(filters, query);
        totalCount = query.Count();
        query = Sort(sorts, query);
        query = query.Skip((pageIndex) * pageSize).Take(pageSize);
        var result = query.ToArray();
        return result; } }

    public bool AlbumHasTracks(int albumId) {
      using (AlbumContext entityContext = new AlbumContext()) {
        var query = (from c in entityContext.AlbumSet
                     where c.AlbumId == albumId && c.Tracks.Count > 0
                     select c).FirstOrDefault();
        if (query == null) return false;
        else return true; } }

    private static IQueryable<Album> Filter(AlbumFilter filters, IQueryable<Album> query) {
      if (!String.IsNullOrWhiteSpace(filters.Title)) query = query
          .Where(t => t.Title.ToLower().Contains(filters.Title.ToLower()));
      if (!String.IsNullOrWhiteSpace(filters.Artist)) query = query
          .Where(a => a.Artist.ToLower().Contains(filters.Artist.ToLower()));
      if (filters.Genre > 0) query = query.Where(g => g.Genre == filters.Genre);
      if (filters.Price >= 0) query = query.Where(g => g.Price == filters.Price);
      if (filters.StockAmount >= 0) query = query.Where(g => g.StockAmount == filters.StockAmount);
      return query; }

    private static IQueryable<Album> Sort(List<SortItem> sorts, IQueryable<Album> query) {
      foreach (var sort in sorts) {
        switch (sort.Column) {
          case "AlbumId": query = query.CustomOrderBy(i => i.AlbumId, sort.Direction); break;
          case "Title": query = query.CustomOrderBy(i => i.Title, sort.Direction); break;
          case "Artist": query = query.CustomOrderBy(i => i.Artist, sort.Direction); break;
          case "Genre": query = query.CustomOrderBy(i => i.Genre, sort.Direction); break;
          case "AlbumNumber": query = query.CustomOrderBy(i => i.AlbumNumber, sort.Direction); break;
          case "Price": query = query.CustomOrderBy(i => i.Price, sort.Direction); break;
          case "StockAmount": query = query.CustomOrderBy(i => i.StockAmount, sort.Direction); break;
          default: break; } }
      return query; }

  }
}
