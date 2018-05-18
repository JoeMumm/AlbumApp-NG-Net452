using AlbumApp.Business.Contracts;
using AlbumApp.Business.Entities;
using AlbumApp.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AlbumApp.Data
{
  public class CartRepository : DataRepositoryBase<CartItem>, ICartRepository
  {
    protected override CartItem AddEntity(AlbumContext entityContext, CartItem entity) {
      return entityContext.CartItemSet.Add(entity); } 

    protected override CartItem UpdateEntity(AlbumContext entityContext, CartItem entity) {
      return (from e in entityContext.CartItemSet
              where e.CartItemId == entity.CartItemId
              select e).FirstOrDefault(); }

    protected override IEnumerable<CartItem> GetEntities(AlbumContext entityContext)
    { return from e in entityContext.CartItemSet select e; }

    protected override CartItem GetEntity(AlbumContext entityContext, int id) {
      var query = (from e in entityContext.CartItemSet where e.CartItemId == id select e);
      var results = query.FirstOrDefault();
      return results; }

    public IEnumerable<CartInfo> GetPendingCartItemsByAccountId(int accountId) {
      using (AlbumContext entityContext = new AlbumContext()) {
        var query = from c in entityContext.CartItemSet
                    join a in entityContext.AlbumSet on c.AlbumId equals a.AlbumId
                    where c.AccountId == accountId && c.OrderId == 1
                    select new CartInfo() { Title = a.Title, Image = a.Image, CartItem = c };
                    return query.ToList(); } }

    public CartItem GetByID(int cartId) {
      using (AlbumContext entityContext = new AlbumContext()) {
        var query = from c in entityContext.CartItemSet
                    where c.CartItemId == cartId select c;
        return query.FirstOrDefault(); } }

    public IEnumerable<CartItem> GetCartItemsByCreatedDate(DateTime created) {
      using (AlbumContext entityContext = new AlbumContext()) {
        var query = from c in entityContext.CartItemSet
                    where c.Created < created && c.OrderId == 1
                    select c;
        return query.ToList(); } }

    public IEnumerable<CartItem> GetAllCartItems() {
      using (AlbumContext entityContext = new AlbumContext()) {
        var query = from c in entityContext.CartItemSet
                    select c;
        return query.ToList(); } }

    public void TransferCartItemsToOrder(int orderId, int AccountId)
    {
      using (AlbumContext entityContext = new AlbumContext())
      {
        IQueryable<CartItem> items = from c in entityContext.CartItemSet
                    where c.AccountId == AccountId && c.OrderId == 1
                    select c;
        foreach (var item in items) item.OrderId = orderId;
        entityContext.SaveChanges();
      }
    }

    public bool CartItemsContainAlbum(int albumId) {
      using (AlbumContext entityContext = new AlbumContext()) {
        var query = (from c in entityContext.CartItemSet
                     where c.AlbumId == albumId select c).FirstOrDefault();
        if (query == null) return false;
        else return true; } }

 
  }
}
