using AlbumApp.Client.Entities;
using AlbumApp.Core.Common.Extensions;
using AlbumApp.Core.Common.Contracts;
using System.ServiceModel;
using System.Threading.Tasks;
using Core.Common.Core;
using System.Collections.Generic;

namespace AlbumApp.Client.Contracts
{
  [ServiceContract]
  public interface IInventoryService : IServiceContract
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
    // TransactionFlow added for safety in case multiple db operations are added later
    // Good to use on all non-retrive, all ouput operations
    [TransactionFlow(TransactionFlowOption.Allowed)]
    Album UpdateAlbum(Album album);

    [OperationContract]
    [TransactionFlow(TransactionFlowOption.Allowed)]
    void DeleteAlbum(int albumId);

    [OperationContract]
    Task<Album> UpdateAlbumAsync(Album album);

    [OperationContract]
    Task DeleteAlbumAsync(int albumId);

    [OperationContract]
    bool AlbumHasTracks(int albumId);
  } 
}
