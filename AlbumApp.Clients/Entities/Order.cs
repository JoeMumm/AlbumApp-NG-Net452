using AlbumApp.Core.Common.Core;
using System;
using System.Collections.Generic;

namespace AlbumApp.Client.Entities
{
  public class Order : ObjectBase
  {
    int _orderId;
    int _accountId;
    decimal _amount;
    decimal _discount;
    decimal _tax;
    decimal _shipping;
    DateTime _created;
    DateTime? _shipped;

    public int OrderId { get { return _orderId; }
      set { if (_orderId != value)
            { _orderId = value; OnPropertyChanged(() => OrderId); } } }
    public int AccountId { get { return _accountId; }
      set { if (_accountId != value)
            { _accountId = value; OnPropertyChanged(() => AccountId); } } }
    public decimal Amount { get { return _amount; }
      set { if (_amount != value)
            { _amount = value; OnPropertyChanged(() => Amount); } } }
    public decimal Discount { get { return _discount; }
      set { if (_discount != value)
            { _discount = value; OnPropertyChanged(() => Discount); } } }
    public decimal Tax { get { return _tax; }
      set { if (_tax != value)
            { _tax = value; OnPropertyChanged(() => Tax); } } }
    public decimal Shipping { get { return _shipping; }
      set { if (_shipping != value)
            { _shipping = value; OnPropertyChanged(() => Shipping); } } }
    public DateTime Created { get { return _created; }
      set { if (_created != value)
            { _created = value; OnPropertyChanged(() => Created); } } }
    public DateTime? Shipped { get { return _shipped; }
      set { if (_shipped != value)
            { _shipped = value; OnPropertyChanged(() => Shipped); } } }

    public ICollection<CartItem> CartItems { get; set; }
  }
}
