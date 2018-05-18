using AlbumApp.Core.Common.Core;
using AlbumApp.Core.Common.Contracts;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace AlbumApp.Business.Entities
{
  [DataContract]
  public class Album : EntityBase, IIdentifiableEntity
  {
    [DataMember]
    public int AlbumId { get; set; }
    [DataMember]
    public string AlbumNumber { get; set; }
    [DataMember]
    public string Title { get; set; }
    [DataMember]
    public int Genre { get; set; }
    [DataMember]
    public string Artist { get; set; }
    [DataMember]
    public decimal Price { get; set; }
    [DataMember]
    public int StockAmount { get; set; }
    [DataMember]
    public string Image { get; set; }

    [DataMember]
    public ICollection<Track> Tracks { get; set; }


    public int EntityId
    { get { return AlbumId; } set { AlbumId = value; } }
  }
}
