using AlbumApp.Client.Entities;
using AlbumApp.Common;
using AlbumApp.Core.Common.Extensions;
using AlbumApp.Core.Common.Contracts;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;
using AlbumApp.Clients.Contracts;

namespace AlbumApp.Client.Contracts
{
  [ServiceContract]
  public interface ICartService : IServiceContract
  {
    [OperationContract]
    [FaultContract(typeof(NotFoundException))]
    [FaultContract(typeof(AuthorizationValidationException))]
    [TransactionFlow(TransactionFlowOption.Allowed)]
    CartItem Add(CartItem cartItem);

    [OperationContract]
    IEnumerable<CartItem> GetCartItems(string loginEmail);

    [OperationContract]
    CartItem[] GetDeadCartItems();

    [OperationContract]
    [FaultContract(typeof(NotFoundException))]
    [FaultContract(typeof(AuthorizationValidationException))]
    [TransactionFlow(TransactionFlowOption.Allowed)]
    void DeleteCartItem(int cartItemId);

    [OperationContract]
    Task DeleteCartItemAsync(int cartItemId);

    [OperationContract]
    void UpdateCartItem(int cartItemId, int quantity);

    [OperationContract]
    IEnumerable<CartInfo> GetPendingCartItemsByAccountId(int accountId);

    [OperationContract]
    [FaultContract(typeof(NotFoundException))]
    [FaultContract(typeof(AuthorizationValidationException))]
    [TransactionFlow(TransactionFlowOption.Allowed)]
    void TransferCartItemsToOrder(int orderId, int AccountId);

    [OperationContract]
    bool CartItemsContainAlbum(int albumId);
  }
}
