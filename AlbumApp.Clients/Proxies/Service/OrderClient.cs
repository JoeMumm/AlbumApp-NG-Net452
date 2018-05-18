using AlbumApp.Client.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using AlbumApp.Client.Entities;
using AlbumAppCore.Common.ServiceModel;

namespace AlbumApp.Client.Proxies
{
  public class OrderClient : UserClientBase<IOrderService>, IOrderService
  {
    public Order Add(Order order)
    { return Channel.Add(order); }

    public Order Get(int orderId)
    { return Channel.Get(orderId); }

    public IEnumerable<Order> GetUsersOrders(string loginEmail)
    {
      return Channel.GetUsersOrders(loginEmail);
    }

  }
}
