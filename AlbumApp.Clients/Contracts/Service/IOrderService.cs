using AlbumApp.Client.Entities;
using AlbumApp.Common;
using AlbumApp.Core.Common.Extensions;
using AlbumApp.Core.Common.Contracts;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;

namespace AlbumApp.Client.Contracts
{
  [ServiceContract]
  public interface IOrderService : IServiceContract
  {
    [OperationContract]
    [FaultContract(typeof(NotFoundException))]
    [FaultContract(typeof(AuthorizationValidationException))]
    [TransactionFlow(TransactionFlowOption.Allowed)]
    Order Add(Order order);

    [OperationContract]
    Order Get(int orderId);

    // Exceptions thrown in methods for the entire class (ManagerBase and the Service) 
    // need FaultContracts on every OperationContract method
    [OperationContract]
    [FaultContract(typeof(AuthorizationValidationException))]
    [FaultContract(typeof(NotFoundException))]
    IEnumerable<Order> GetUsersOrders(string loginEmail);

  }
}
