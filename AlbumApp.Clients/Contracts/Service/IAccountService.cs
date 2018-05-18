using AlbumApp.Client.Entities;
using AlbumApp.Common;
using AlbumApp.Core.Common.Extensions;
using AlbumApp.Core.Common.Contracts;
using System.ServiceModel;

namespace AlbumApp.Client.Contracts
{
  [ServiceContract]
  public interface IAccountService : IServiceContract
  {
    [OperationContract]
    [FaultContract(typeof(NotFoundException))]
    [FaultContract(typeof(AuthorizationValidationException))]
    Account GetCustomerAccountInfo(string loginEmail);

    [OperationContract]
    [FaultContract(typeof(AuthorizationValidationException))]
    [TransactionFlow(TransactionFlowOption.Allowed)]
    void UpdateCustomerAccountInfo(Account account);
  }
}