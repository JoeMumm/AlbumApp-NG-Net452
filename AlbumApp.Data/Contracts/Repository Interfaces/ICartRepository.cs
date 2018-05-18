using AlbumApp.Business.Contracts;
using AlbumApp.Business.Entities;
using AlbumApp.Core.Common.Contracts;
using System;
using System.Collections.Generic;

namespace AlbumApp.Data.Contracts
{
  public interface ICartRepository : IDataRepository<CartItem>
  {
    IEnumerable<CartItem> GetCartItemsByCreatedDate(DateTime created);
    CartItem GetByID(int cartId);
    IEnumerable<CartItem> GetAllCartItems();
    IEnumerable<CartInfo> GetPendingCartItemsByAccountId(int accountId);
    void TransferCartItemsToOrder(int orderId, int AccountId);
    bool CartItemsContainAlbum(int albumId);
  }
}
