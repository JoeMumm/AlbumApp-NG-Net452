using AlbumApp.Client.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using AlbumApp.Client.Entities;
using AlbumAppCore.Common.ServiceModel;
using Core.Common.Core;

namespace AlbumApp.Client.Proxies
{
  public class InventoryClient : UserClientBase<IInventoryService>, IInventoryService
  {
    public void DeleteAlbum(int albumId)
    {
      Channel.DeleteAlbum(albumId);
    }

    public Task DeleteAlbumAsync(int albumId)
    {
      return Channel.DeleteAlbumAsync(albumId);
    }

    public Album GetAlbum(int albumId)
    {
      return Channel.GetAlbum(albumId);
    }

    public Album[] GetAllAlbums()
    {
      return Channel.GetAllAlbums();
    }

    public Album[] GetAllAlbumsPaged(out int totalCount, AlbumFilter filters,
      List<SortItem> sorts, int pageIndex, int pageSize)
    {
      return Channel.GetAllAlbumsPaged(out totalCount, filters,
        sorts, pageIndex, pageSize);
    }

    public Album GetAlbumTracksById(int albumId) {
      return Channel.GetAlbumTracksById(albumId);
    }

    public Album UpdateAlbum(Album album)
    {
      return Channel.UpdateAlbum(album);
    }

    public Task<Album> UpdateAlbumAsync(Album album)
    {
      return Channel.UpdateAlbumAsync(album);
    }

    public bool AlbumHasTracks(int albumId)
    {
      return Channel.AlbumHasTracks(albumId);
    }
  }
}
