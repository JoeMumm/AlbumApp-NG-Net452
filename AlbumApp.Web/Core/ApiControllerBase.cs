using AlbumApp.Common;
using AlbumApp.Core.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Security;
using System.ServiceModel;
using System.Web.Http;

namespace AlbumApp.Web.Core
{
  public class ApiControllerBase : ApiController, IServiceAwareController
  {
    List<IServiceContract> _DisposableServices;

    protected virtual void RegisterServices(List<IServiceContract> disposableServices) { }

    void IServiceAwareController.RegisterDisposableServices(List<IServiceContract> disposableServices)
    { RegisterServices(disposableServices); }

    List<IServiceContract> IServiceAwareController.DisposableServices {
        get { if (_DisposableServices == null)
                _DisposableServices = new List<IServiceContract>();

            return _DisposableServices; } }

    protected void ValidateAuthorizedUser(string userRequested) {
        string userLoggedIn = User.Identity.Name;
        if (userLoggedIn != userRequested)
            throw new SecurityException("Attempting to access data for another user."); }

    protected IHttpActionResult GetHttpResponse(HttpRequestMessage request,
                                          Func<IHttpActionResult> codeToExecute) {
      IHttpActionResult response = null;

      try { response = codeToExecute.Invoke(); }
      catch (SecurityException ex)
      { response = Content(HttpStatusCode.Unauthorized, ex.Message); }
      catch (FaultException<AuthorizationValidationException> ex)
      { response = Content(HttpStatusCode.Unauthorized, ex.Message); }
      catch (FaultException ex)
      { response = Content(HttpStatusCode.InternalServerError, ex.Message); }
      catch (Exception ex)
      { response = Content(HttpStatusCode.InternalServerError, ex.Message); }

        return response; }
  }
}