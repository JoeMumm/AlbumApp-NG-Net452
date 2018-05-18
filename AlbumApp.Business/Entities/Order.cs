using AlbumApp.Core.Common.Core;
using AlbumApp.Core.Common.Contracts;
using System;
using System.Runtime.Serialization;
using System.Collections.Generic;

namespace AlbumApp.Business.Entities
{
  [DataContract]
  public class Order : EntityBase, IIdentifiableEntity, IAccountOwnedEntity
  {
    [DataMember]
    public int OrderId { get; set; }
    [DataMember]
    public int AccountId { get; set; }
    [DataMember]
    public decimal Amount { get; set; }
    [DataMember]
    public decimal Discount { get; set; }
    [DataMember]
    public decimal Tax { get; set; }
    [DataMember]
    public decimal Shipping { get; set; }
    [DataMember]
    public DateTime Created { get; set; }
    [DataMember]
    public DateTime? Shipped { get; set; }

    [DataMember]
    public ICollection<CartItem> CartItems { get; set; }

    public int EntityId
    { get { return OrderId; } set { OrderId = value; } }


    int IAccountOwnedEntity.OwnerAccountId
    { get { return AccountId; } }
  }
}
