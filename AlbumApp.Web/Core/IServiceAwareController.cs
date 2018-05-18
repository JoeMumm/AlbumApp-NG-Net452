using AlbumApp.Core.Common.Contracts;
using System.Collections.Generic;

namespace AlbumApp.Web.Core
{
  public interface IServiceAwareController
    {
        void RegisterDisposableServices(List<IServiceContract> disposableServices);

        List<IServiceContract> DisposableServices { get; }
    }
}
