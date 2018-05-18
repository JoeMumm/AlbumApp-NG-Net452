using AlbumApp.Business.Entities;
using AlbumApp.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AlbumApp.Data
{
  public class OrderRepository : DataRepositoryBase<Order>, IOrderRepository
  {
    protected override Order AddEntity(AlbumContext entityContext, Order entity) {
      return entityContext.OrderSet.Add(entity); } 

    protected override Order UpdateEntity(AlbumContext entityContext, Order entity) {
      return (from e in entityContext.OrderSet
              where e.OrderId == entity.OrderId
              select e).FirstOrDefault(); }

    protected override IEnumerable<Order> GetEntities(AlbumContext entityContext)
    { return from e in entityContext.OrderSet select e; }

    protected override Order GetEntity(AlbumContext entityContext, int id) {
      var query = (from e in entityContext.OrderSet where e.OrderId == id select e);
      var results = query.FirstOrDefault();
      return results; }
    
    public IEnumerable<Order> GetCustomerOrders(int accountId) {
      using (AlbumContext entityContext = new AlbumContext()) {
        var query = from o in entityContext.OrderSet
                    where o.AccountId == accountId select o;
        return query.ToList(); } }

    public IEnumerable<CustomerPurchaseInfo> GetCurrentCustomerPurchaseInfo() {
      using (AlbumContext entityContext = new AlbumContext()) {
        var query = from c in entityContext.CartItemSet
           join u in entityContext.Users on c.AccountId equals u.AccountSet.AccountId
           join a in entityContext.AlbumSet on c.AlbumId equals a.AlbumId
           select new CustomerPurchaseInfo() { Customer = u.AccountSet,
            CartItem = c, Album = a };
        return query.ToList(); } }


  }
}
