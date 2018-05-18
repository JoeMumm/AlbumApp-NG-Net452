using AlbumApp.Business.Contracts;
using AlbumApp.Business.Entities;
using AlbumApp.Common;
using AlbumApp.Core.Common.Contracts;
using AlbumApp.Core.Common.Extensions;
using AlbumApp.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Security.Permissions;
using Core.Common.Core;
using AlbumApp.Data;

namespace AlbumApp.Business.Managers
{
  [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
        ConcurrencyMode = ConcurrencyMode.Multiple,
        ReleaseServiceInstanceOnTransactionComplete = false)]
  public class InventoryManager : ManagerBase, IInventoryService {

    public InventoryManager() { }

    public InventoryManager(IDataRepositoryFactory dataRepositoryFactory)
    { _dataRepositoryFactory = dataRepositoryFactory; }

    public Album GetAlbum(int albumId) {
      return ExecuteFaultHandledOperation(() => {
        IAlbumRepository albumRepository =
              _dataRepositoryFactory.GetDataRepository<IAlbumRepository>();
        Album albumEntity = albumRepository.GetById(albumId);
        if (albumEntity == null) {
          NotFoundException ex =
            new NotFoundException(String.Format($"Album with ID of {albumId} is not in the database."));
          throw new FaultException<NotFoundException>(ex, ex.Message); }
        return albumEntity; }); }

    public Album[] GetAllAlbums() {
      try { IAlbumRepository albumRepository =
              _dataRepositoryFactory.GetDataRepository<IAlbumRepository>();
        IEnumerable<Album> albums = albumRepository.Get();
        return albums.ToArray(); }
      catch (FaultException ex) { throw ex; }
      catch (Exception ex) { throw new FaultException(ex.Message); } }
    
    public Album[] GetAllAlbumsPaged(out int totalCount, AlbumFilter filters, List<SortItem> sorts,
      int pageIndex, int pageSize) {
      try { IAlbumRepository albumRepository =
              _dataRepositoryFactory.GetDataRepository<IAlbumRepository>();
        IEnumerable<Album> albums = albumRepository.GetAllAlbumsPaged(out totalCount, filters, sorts,
          pageIndex, pageSize);
        return albums.ToArray(); }
      catch (FaultException ex) { throw ex; }
      catch (Exception ex) { throw new FaultException(ex.Message); } }

    public Album GetAlbumTracksById(int albumId) {
      return ExecuteFaultHandledOperation(() => {
        IAlbumRepository albumRepository =
            _dataRepositoryFactory.GetDataRepository<IAlbumRepository>();
        Album albums = albumRepository.GetAlbumTracksById(albumId);
        if (albums == null) {
          NotFoundException ex =
            new NotFoundException(String.Format($"Album with ID of {albumId} is not in the database."));
          throw new FaultException<NotFoundException>(ex, ex.Message); }
        return albums; }); }
    
    [OperationBehavior(TransactionScopeRequired = true)]
    [PrincipalPermission(SecurityAction.Demand, Role = Security.AlbumAppAdminRole)]
    public Album UpdateAlbum(Album album) {
      return ExecuteFaultHandledOperation(() => { IAlbumRepository albumRepository =
              _dataRepositoryFactory.GetDataRepository<IAlbumRepository>();
        Album updatedEntity = null;
        if (album.AlbumId == 0) updatedEntity = albumRepository.Add(album);
        else updatedEntity = albumRepository.Update(album);
        return updatedEntity; }); }
    
    
    [OperationBehavior(TransactionScopeRequired = true)]
    [PrincipalPermission(SecurityAction.Demand, Role = Security.AlbumAppAdminRole)]
    public void DeleteAlbum(int albumId) {
      ExecuteFaultHandledOperation(() => { IAlbumRepository albumRepository =
              _dataRepositoryFactory.GetDataRepository<IAlbumRepository>();
        albumRepository.Remove(albumId); }); }

    [PrincipalPermission(SecurityAction.Demand, Role = Security.AlbumAppAdminRole)]
    public bool AlbumHasTracks(int albumId) { bool hasTracks = true;
      ExecuteFaultHandledOperation(() => {
        IAlbumRepository albumRepository =
              _dataRepositoryFactory.GetDataRepository<IAlbumRepository>();
        hasTracks = albumRepository.AlbumHasTracks(albumId); });
      return hasTracks; }
  }
}
