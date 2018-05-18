using AlbumApp.Core.Common.Core;
using AlbumApp.Core.Common.Contracts;
using System;
using System.Runtime.Serialization;

namespace AlbumApp.Business.Entities
{
  [DataContract]
  public class CartItem : EntityBase, IIdentifiableEntity, IAccountOwnedEntity
  {
    [DataMember]
    public int CartItemId { get; set; }
    [DataMember]
    public int AccountId { get; set; }
    [DataMember]
    public int AlbumId { get; set; }
    [DataMember]
    public int Quantity { get; set; }
    [DataMember]
    public decimal Price { get; set; }
    [DataMember]
    public DateTime Created { get; set; }

    [DataMember]
    public int OrderId { get; set; }

    public Order Order { get; set; }

    public int EntityId
    { get { return CartItemId; } set { CartItemId = value; } }

    int IAccountOwnedEntity.OwnerAccountId
    { get { return AccountId; } }
  }
}
