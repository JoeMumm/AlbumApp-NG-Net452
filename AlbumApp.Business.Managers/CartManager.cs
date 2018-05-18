using AlbumApp.Business.Contracts;
using AlbumApp.Business.Entities;
using AlbumApp.Common;
using AlbumApp.Core.Common.Contracts;
using AlbumApp.Core.Common.Extensions;
using AlbumApp.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.ServiceModel;

namespace AlbumApp.Business.Managers
{
  [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
        ConcurrencyMode = ConcurrencyMode.Multiple,
        ReleaseServiceInstanceOnTransactionComplete = false)]
  public class CartManager : ManagerBase, ICartService {

    public CartManager() {  }

    public CartManager(IDataRepositoryFactory dataRepositoryFactory)
    { _dataRepositoryFactory = dataRepositoryFactory; }
    
    protected override int GetAccountId(string loginName) {
      return GetAccntId( loginName); }
    
    [OperationBehavior(TransactionScopeRequired = true)]
    [PrincipalPermission(SecurityAction.Demand, Role = Security.AlbumAppAdminRole)]
    [PrincipalPermission(SecurityAction.Demand, Name = Security.AlbumAppUser)]
    public CartItem Add(CartItem cartItem) {
      return ExecuteFaultHandledOperation(() => {
        ValidationAuthorization(((IAccountOwnedEntity) cartItem).OwnerAccountId); // GetOwnerAccountId(cartItem)
        ICartRepository cartRepository =
            _dataRepositoryFactory.GetDataRepository<ICartRepository>();
        CartItem newCartItem = cartRepository.Add(cartItem);
        return newCartItem; }); }
    
    public IEnumerable<CartItem> GetCartItems(string loginEmail)
    { throw new NotImplementedException(); }

    [PrincipalPermission(SecurityAction.Demand, Role = Security.AlbumAppAdminRole)]
    public CartItem[] GetDeadCartItems() {
      return ExecuteFaultHandledOperation(() => {
        ICartRepository cartRepository =
            _dataRepositoryFactory.GetDataRepository<ICartRepository>();
        IEnumerable<CartItem> cartItems =
            cartRepository.GetCartItemsByCreatedDate(DateTime.Now.AddDays(-7));
        return cartItems.ToArray(); }); }//ToList().Where(i => i.OrderId == 1).
     
    
    [OperationBehavior(TransactionScopeRequired = true)]
    [PrincipalPermission(SecurityAction.Demand, Role = Security.AlbumAppAdminRole)]
    [PrincipalPermission(SecurityAction.Demand, Name = Security.AlbumAppUser)]
    public void DeleteCartItem(int cartItemId) {
      ExecuteFaultHandledOperation(() => {
        ICartRepository cartRepository =
            _dataRepositoryFactory.GetDataRepository<ICartRepository>();
        CartItem cartItem = cartRepository.Get(cartItemId);
        if (cartItem == null) {
          NotFoundException ex =
            new NotFoundException(string.Format($"No cart item found for ID '{cartItemId}'."));
          throw new FaultException<NotFoundException>(ex, ex.Message); }
        ValidationAuthorization(((IAccountOwnedEntity)cartItem).OwnerAccountId); // GetOwnerAccountId(cartItem)
        cartRepository.Remove(cartItem); }); }
    
    [OperationBehavior(TransactionScopeRequired = true)]
    [PrincipalPermission(SecurityAction.Demand, Role = Security.AlbumAppAdminRole)]
    [PrincipalPermission(SecurityAction.Demand, Name = Security.AlbumAppUser)]
    public void UpdateCartItem(int cartItemId, int quantity) {
      ExecuteFaultHandledOperation(() => {
        ICartRepository cartRepository =
            _dataRepositoryFactory.GetDataRepository<ICartRepository>();
        CartItem cartItem = cartRepository.Get(cartItemId);
        if (cartItem == null) {
          NotFoundException ex =
            new NotFoundException(string.Format($"No cart item found for ID '{cartItemId}'."));
          throw new FaultException<NotFoundException>(ex, ex.Message); }
        ValidationAuthorization(((IAccountOwnedEntity) cartItem).OwnerAccountId);  //GetOwnerAccountId(cartItem)

        cartItem.Quantity = quantity;
        var updatedEntity = cartRepository.Update(cartItem); }); }

    [PrincipalPermission(SecurityAction.Demand, Role = Security.AlbumAppAdminRole)]
    [PrincipalPermission(SecurityAction.Demand, Name = Security.AlbumAppUser)]
    public IEnumerable<CartInfo> GetPendingCartItemsByAccountId(int accountId) {
      return ExecuteFaultHandledOperation(() => {
        ValidationAuthorization(accountId);

        ICartRepository cartRepository =
                  _dataRepositoryFactory.GetDataRepository<ICartRepository>();
        IEnumerable<CartInfo> cartItems =
                  cartRepository.GetPendingCartItemsByAccountId(accountId);
        return cartItems.ToList(); }); }

    [OperationBehavior(TransactionScopeRequired = true)]
    [PrincipalPermission(SecurityAction.Demand, Role = Security.AlbumAppAdminRole)]
    [PrincipalPermission(SecurityAction.Demand, Name = Security.AlbumAppUser)]
    public void TransferCartItemsToOrder(int orderId, int accountId) {
      ExecuteFaultHandledOperation(() => {
        IOrderRepository orderRepository =
                _dataRepositoryFactory.GetDataRepository<IOrderRepository>();
        Order order = orderRepository.Get(orderId);
        ValidationAuthorization(((IAccountOwnedEntity)order).OwnerAccountId); // GetOwnerAccountId(order)
        ValidationAuthorization(accountId);

        ICartRepository cartRepository =
            _dataRepositoryFactory.GetDataRepository<ICartRepository>();
        cartRepository.TransferCartItemsToOrder(orderId, accountId); }); }

    [PrincipalPermission(SecurityAction.Demand, Role = Security.AlbumAppAdminRole)]
    public bool CartItemsContainAlbum(int albumId) { bool hasCartItems = true;
      ExecuteFaultHandledOperation(() => {
        ICartRepository cartRepository =
            _dataRepositoryFactory.GetDataRepository<ICartRepository>();
        hasCartItems = cartRepository.CartItemsContainAlbum(albumId); });
      return hasCartItems; }
  }
}
