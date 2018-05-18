using AlbumApp.Core.Common.Contracts;
using AlbumApp.Core.Common.Core;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace AlbumApp.Business.Entities
{
  [DataContract]
  public class Account : EntityBase, IIdentifiableEntity, IAccountOwnedEntity
  {
    [DataMember]
    [Key, ForeignKey("User")]
    public string Id { get; set; }

    [DataMember]
    public int AccountId { get; set; }

    [DataMember]
    public string FirstName { get; set; }

    [DataMember]
    public string LastName { get; set; }

    [DataMember]
    public string Address { get; set; }

    [DataMember]
    public string City { get; set; }

    [DataMember]
    public string State { get; set; }

    [DataMember]
    public string ZipCode { get; set; }

    [DataMember]
    public string CreditCard { get; set; }

    [DataMember]
    public string ExpDate { get; set; }

    public virtual ApplicationUser User { get; set; }


    public int EntityId
    { get { return AccountId; } set { AccountId = value; } }

    public int OwnerAccountId { get { return AccountId; } }
  }
}
