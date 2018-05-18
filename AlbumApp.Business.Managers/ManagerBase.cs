using AlbumApp.Business.Entities;
using AlbumApp.Common;
using AlbumApp.Core.Common.Contracts;
using AlbumApp.Core.Common.Extensions;
using AlbumApp.Data;
using AlbumApp.Data.Contracts;
using System;
using System.ServiceModel;
using System.Threading;

namespace AlbumApp.Business.Managers
{
  public class ManagerBase
  {
    public ManagerBase()
    {
      OperationContext context = OperationContext.Current;
      if (context != null) { _loginName =
          context.IncomingMessageHeaders.GetHeader<string>("String", "System");
        if (_loginName.IndexOf(@"\") > -1) _loginName = string.Empty;  }

      _dataRepositoryFactory = new DataRepositoryFactory();
      
      if (!string.IsNullOrWhiteSpace(_loginName))
        _authorizationAccountId = GetAccountId(_loginName);
    }

    protected IDataRepositoryFactory _dataRepositoryFactory;

    protected int _authorizationAccountId = 0;
    protected string _loginName;

    protected virtual int GetAccountId(string loginName) {
      return 0;
    }

    protected int GetAccntId(string loginName) {
      IAccountRepository accountRepository =
                      _dataRepositoryFactory.GetDataRepository<IAccountRepository>();
      Account authAcct = accountRepository.GetByLogin(loginName);
      if (authAcct == null) {
        NotFoundException ex = new NotFoundException(string.Format(
          $"Cannot find account for login name {loginName} to use for security check."));
        throw new FaultException<NotFoundException>(ex, ex.Message); }
      return authAcct.AccountId; }

    protected void ValidationAuthorization(int accountId) {
      var isAdmin = Thread.CurrentPrincipal.IsInRole(Security.AlbumAppAdminRole); 
      //isAdmin = false; // for testing only
      if (!isAdmin)
       if (_authorizationAccountId != 0)
        if (_loginName != string.Empty && accountId != _authorizationAccountId) {
          AuthorizationValidationException ex =  new AuthorizationValidationException(
                              "Unauthorized attempt to access a secure resource");
          throw new FaultException<AuthorizationValidationException>(ex, ex.Message); }   }

    protected T ExecuteFaultHandledOperation<T>(Func<T> codeToExecute) {
      try { return codeToExecute.Invoke(); }
      catch (FaultException ex) { throw ex; }
      catch (Exception ex) { throw new FaultException(ex.Message); } }

    protected void ExecuteFaultHandledOperation(Action codeToExecute) {
      try { codeToExecute.Invoke(); }
      catch (FaultException ex) { throw ex; }
      catch (Exception ex) { throw new FaultException(ex.Message); } }

  }
}
