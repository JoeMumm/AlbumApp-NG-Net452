using System.Runtime.Serialization;

namespace AlbumApp.Core.Common.Core
{
  [DataContract]
  public abstract class EntityBase : IExtensibleDataObject
  {
    public ExtensionDataObject ExtensionData { get; set; }

  }
}
