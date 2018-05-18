using AlbumApp.Business.Contracts;
using AlbumApp.Business.Entities;
using AlbumApp.Common;
using AlbumApp.Data.Contracts;
using AlbumApp.Core.Common.Contracts;
using AlbumApp.Core.Common.Extensions;
using System.Security.Permissions;
using System.ServiceModel;

namespace AlbumApp.Business.Managers
{
  [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
       ConcurrencyMode = ConcurrencyMode.Multiple,
       ReleaseServiceInstanceOnTransactionComplete = false)]
  public class AccountManager : ManagerBase, IAccountService {

    public AccountManager() { }

    public AccountManager(IDataRepositoryFactory dataRepositoryFactory)
    { _dataRepositoryFactory = dataRepositoryFactory; }

    [PrincipalPermission(SecurityAction.Demand, Role = Security.AlbumAppAdminRole)]
    [PrincipalPermission(SecurityAction.Demand, Name = Security.AlbumAppUser)]
    public Account GetCustomerAccountInfo(string loginEmail)
    {
      return ExecuteFaultHandledOperation(() => {
        IAccountRepository accountRepository = _dataRepositoryFactory.GetDataRepository<IAccountRepository>();
        Account accountEntity = accountRepository.GetByLogin(loginEmail);
        if (accountEntity == null) { NotFoundException ex =
            new NotFoundException(string.Format($"Account with login '{loginEmail}' is not in database"));
          throw new FaultException<NotFoundException>(ex, ex.Message);
        }
        return accountEntity;
      });
    }

    [OperationBehavior(TransactionScopeRequired = true)]
    [PrincipalPermission(SecurityAction.Demand, Role = Security.AlbumAppAdminRole)]
    [PrincipalPermission(SecurityAction.Demand, Name = Security.AlbumAppUser)]
    public void UpdateCustomerAccountInfo(Account account)
    {
      ExecuteFaultHandledOperation(() => {
        IAccountRepository accountRepository = _dataRepositoryFactory.GetDataRepository<IAccountRepository>();
        Account updatedAccount = accountRepository.Update(account);
      });
    }

  }
}
