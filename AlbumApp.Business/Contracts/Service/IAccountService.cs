using AlbumApp.Business.Entities;
using AlbumApp.Common;
using AlbumApp.Core.Common.Extensions;
using System.ServiceModel;

namespace AlbumApp.Business.Contracts
{
  [ServiceContract]
  public interface IAccountService
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