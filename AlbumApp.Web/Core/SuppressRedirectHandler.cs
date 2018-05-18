using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AlbumApp.Web.Core
{
  public class SuppressRedirectHandler : DelegatingHandler
  {
    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
          CancellationToken cancellationToken) {
      return base.SendAsync(request, cancellationToken).ContinueWith(task => {
        var response = task.Result;
        response.Headers.Add("Suppress-Redirect", "True");
        return response; }, cancellationToken); }
  }
}
