using AlbumApp.Core.Common.Core;
using System;

namespace AlbumApp.Client.Entities
{
  public class CartItem : ObjectBase
  {
    int _cartItemId;
    int _accountId;
    int _albumId;
    int _quantity;
    decimal _price;
    DateTime _created;

    int _orderId;

    public int CartItemId { get { return _cartItemId; }
      set { if (_cartItemId != value)
            { _cartItemId = value; OnPropertyChanged(() => CartItemId); } } }
    public int AccountId { get { return _accountId; }
      set { if (_accountId != value)
            { _accountId = value; OnPropertyChanged(() => AccountId); } } }
    public int AlbumId { get { return _albumId; }
      set { if (_albumId != value)
            { _albumId = value; OnPropertyChanged(() => AlbumId); } } }
    public int Quantity { get { return _quantity; }
      set { if (_quantity != value)
            { _quantity = value; OnPropertyChanged(() => Quantity); } } }
    public decimal Price { get { return _price; }
      set { if (_price != value)
            { _price = value; OnPropertyChanged(() => Price); } } }
    public DateTime Created { get { return _created; }
      set { if (_created != value)
            { _created = value; OnPropertyChanged(() => Created); } } }

    public int OrderId { get { return _orderId; }
      set { if (_orderId != value)
            { _orderId = value; OnPropertyChanged(() => OrderId); } } }

    public Order Order { get; set; }

  }
}
