using AlbumApp.Client.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlbumApp.Admin.Support
{
  public class AlbumEventArgs : EventArgs
  {
    public AlbumEventArgs(Album album, bool isNew)
    { Album = album; IsNew = isNew; }

    public Album Album { get; set; }
    public bool IsNew { get; set; }
  }
}
