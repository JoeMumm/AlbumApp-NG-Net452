using AlbumApp.Business.Entities;
using AlbumApp.Core.Common.Extensions;
using Core.Common.Core;
using System.Collections.Generic;
using System.ServiceModel;

namespace AlbumApp.Business.Contracts
{
  [ServiceContract]
  public interface IInventoryService
  {
    [OperationContract]
    [FaultContract(typeof(NotFoundException))]
    // might use TransactionFlow if doing auditing or logging
    Album GetAlbum(int albumId);

    [OperationContract]
    Album[] GetAllAlbums();

    [OperationContract]
    Album[] GetAllAlbumsPaged(out int totalCount, AlbumFilter filters, List<SortItem> sorts,
      int pageIndex, int pageSize);

    [OperationContract]
    Album GetAlbumTracksById(int albumId);

    [OperationContract]
    // TransactionFlow added for safety in case multiple
    // db operations are added later
    // Good to use on all non-retrive, all ouput operations
    [TransactionFlow(TransactionFlowOption.Allowed)]
    Album UpdateAlbum(Album album);

    [OperationContract]
    [TransactionFlow(TransactionFlowOption.Allowed)]
    void DeleteAlbum(int albumId);

    [OperationContract]
    bool AlbumHasTracks(int albumId);
  }
}
