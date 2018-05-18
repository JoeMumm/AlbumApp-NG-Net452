using AlbumApp.Business.Entities;
using AlbumApp.Core.Common.Contracts;
using System;
using System.Collections.Generic;

namespace AlbumApp.Data.Contracts
{
  public interface IOrderRepository : IDataRepository<Order>
  {
    IEnumerable<Order> GetCustomerOrders(int accountId);
    IEnumerable<CustomerPurchaseInfo> GetCurrentCustomerPurchaseInfo();
  }
}
