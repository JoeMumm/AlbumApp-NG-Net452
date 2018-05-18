using AlbumApp.Client.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlbumApp.Web.Models
{
  public class AlbumDetailsVM
  {
    public int AlbumId { get; set; }
    public string Title { get; set; }
    public string Genre { get; set; }
    public string Artist { get; set; }
    public decimal Price { get; set; }
    public string Image { get; set; }

    public IEnumerable<Track> Tracks { get; set; }
  }
}