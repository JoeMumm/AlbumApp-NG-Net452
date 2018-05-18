using AlbumApp.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlbumApp.Data.Contracts
{
  public class CustomerPurchaseInfo
  {
    public Account Customer { get; set; }
    public Album Album { get; set; }
    public CartItem CartItem { get; set; }
  }
}
