using AlbumApp.Business.Contracts;
using AlbumApp.Business.Entities;
using AlbumApp.Core.Common.Contracts;
using Core.Common.Core;
using System.Collections;
using System.Collections.Generic;

namespace AlbumApp.Data.Contracts
{
  public interface IAlbumRepository : IDataRepository<Album>
  {
    Album GetById(int albumId);

    Album GetAlbumTracksById(int albumId);

    IEnumerable<Album> GetAllAlbumsPaged(out int totalCount, AlbumFilter filters, List<SortItem> sorts,
      int pageIndex, int pageSize);

    bool AlbumHasTracks(int albumId);
  }
}
