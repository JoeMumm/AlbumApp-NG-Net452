using AlbumApp.Business.Contracts;
using AlbumApp.Business.Entities;
using AlbumApp.Common;
using AlbumApp.Core.Common.Contracts;
using AlbumApp.Core.Common.Extensions;
using AlbumApp.Data.Contracts;
using System.Collections.Generic;
using System.Security.Permissions;
using System.ServiceModel;

namespace AlbumApp.Business.Managers
{
  [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
        ConcurrencyMode = ConcurrencyMode.Multiple,
        ReleaseServiceInstanceOnTransactionComplete = false)]
  public class OrderManager : ManagerBase, IOrderService {

    public OrderManager() {  }

    public OrderManager(IDataRepositoryFactory dataRepositoryFactory)
    { _dataRepositoryFactory = dataRepositoryFactory; }

    protected override int GetAccountId(string loginName) {
      return GetAccntId( loginName); }

    [OperationBehavior(TransactionScopeRequired = true)]
    [PrincipalPermission(SecurityAction.Demand, Role = Security.AlbumAppAdminRole)]
    [PrincipalPermission(SecurityAction.Demand, Name = Security.AlbumAppUser)]
    public Order Add(Order order) {
      return ExecuteFaultHandledOperation(() => {
        ValidationAuthorization(((IAccountOwnedEntity) order).OwnerAccountId); // GetOwnerAccountId(order)
        IOrderRepository orderRepository =
            _dataRepositoryFactory.GetDataRepository<IOrderRepository>();
        Order neworder = orderRepository.Add(order);
        return neworder; }); }
    
    [PrincipalPermission(SecurityAction.Demand, Role = Security.AlbumAppAdminRole)]
    [PrincipalPermission(SecurityAction.Demand, Name = Security.AlbumAppUser)]
    public Order Get(int orderId) {
      return ExecuteFaultHandledOperation(() => {
        IOrderRepository orderRepository =
                _dataRepositoryFactory.GetDataRepository<IOrderRepository>();
        Order order = orderRepository.Get(orderId);
        ValidationAuthorization(((IAccountOwnedEntity) order).OwnerAccountId);  // GetOwnerAccountId(order)
        return order; }); }

    [PrincipalPermission(SecurityAction.Demand, Role = Security.AlbumAppAdminRole)]
    [PrincipalPermission(SecurityAction.Demand, Name = Security.AlbumAppUser)]
    public IEnumerable<Order> GetUsersOrders(string loginEmail) {
      return ExecuteFaultHandledOperation(() => {
        var accountId = GetAccountId(loginEmail);
        ValidationAuthorization(accountId);

        IOrderRepository orderRepository =
              _dataRepositoryFactory.GetDataRepository<IOrderRepository>();
        IEnumerable<Order> orders = orderRepository.GetCustomerOrders(accountId);
        return orders; }); }


  }
}
