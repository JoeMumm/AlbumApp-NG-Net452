using AlbumApp.Core.Common.Core;
using AlbumApp.Core.Common.Contracts;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace AlbumApp.Business.Entities
{
  [DataContract]
  public class Track : EntityBase, IIdentifiableEntity
  {
    [DataMember]
    public int TrackId { get; set; }
    [DataMember]
    public string Name { get; set; }
    [DataMember]
    public int Number { get; set; }
    [DataMember]
    [Range(0, 59)]
    public int Minutes { get; set; }
    [DataMember]
    [Range(0, 59)]
    public int Seconds { get; set; }

    [DataMember]
    public int AlbumId { get; set; }
   
    
    public int EntityId
    { get { return TrackId; } set { TrackId = value; } }
  }
}
