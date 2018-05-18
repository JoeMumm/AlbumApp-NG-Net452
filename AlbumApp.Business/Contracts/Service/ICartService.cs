using AlbumApp.Business.Entities;
using AlbumApp.Common;
using AlbumApp.Core.Common.Extensions;
using System.Collections.Generic;
using System.ServiceModel;

namespace AlbumApp.Business.Contracts
{
  [ServiceContract]
  public interface ICartService
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
    IEnumerable<CartInfo> GetPendingCartItemsByAccountId(int accountId);

    [OperationContract]
    void UpdateCartItem(int cartItemId, int quantity);

    [OperationContract]
    [FaultContract(typeof(NotFoundException))]
    [FaultContract(typeof(AuthorizationValidationException))]
    [TransactionFlow(TransactionFlowOption.Allowed)]
    void TransferCartItemsToOrder(int orderId, int AccountId);

    [OperationContract]
    bool CartItemsContainAlbum(int albumId);
  }
}
