using AlbumApp.Client.Contracts;
using AlbumApp.Client.Entities;
using AlbumApp.Clients.Contracts;
using AlbumAppCore.Common.ServiceModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AlbumApp.Client.Proxies
{
  public class CartClient : UserClientBase<ICartService>, ICartService
  {
    public CartItem Add(CartItem cartItem)
    { return Channel.Add(cartItem); }

    public void DeleteCartItem(int cartItemId)
    {
      Channel.DeleteCartItem(cartItemId);
    }

    public Task DeleteCartItemAsync(int cartItemId)
    {
      return Channel.DeleteCartItemAsync(cartItemId);
    }

    public IEnumerable<CartItem> GetCartItems(string loginEmail)
    {
      return Channel.GetCartItems(loginEmail);
    }

    public CartItem[] GetDeadCartItems()
    {
      return Channel.GetDeadCartItems();
    }

    public void UpdateCartItem(int cartItemId, int quantity)
    {
      Channel.UpdateCartItem(cartItemId, quantity);
    }

    public IEnumerable<CartInfo> GetPendingCartItemsByAccountId(int accountId)
    {
      return Channel.GetPendingCartItemsByAccountId(accountId);
    }

    public void TransferCartItemsToOrder(int orderId, int AccountId)
    {
      Channel.TransferCartItemsToOrder(orderId, AccountId);
    }

    public bool CartItemsContainAlbum(int albumId)
    {
      return Channel.CartItemsContainAlbum(albumId);
    }
  }
}
