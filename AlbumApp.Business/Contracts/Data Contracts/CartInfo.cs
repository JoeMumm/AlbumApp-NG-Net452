using AlbumApp.Business.Entities;
using Core.Common.ServiceModel;
using System.Runtime.Serialization;

namespace AlbumApp.Business.Contracts
{
  [DataContract]
  public class CartInfo : DataContractBase
  {
    [DataMember]
    public string Title { get; set; }
    [DataMember]
    public string Image { get; set; }
    [DataMember]
    public CartItem CartItem { get; set; }
  }
}
