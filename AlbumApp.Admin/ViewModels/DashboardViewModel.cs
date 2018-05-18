using AlbumApp.Core.Common.Contracts;
using AlbumApp.Core.Common.UI.Core;

namespace AlbumApp.Admin.ViewModels
{
  public class DashboardViewModel : ViewModelBase
  {
    private IServiceFactory _serviceFactory;

    public DashboardViewModel(IServiceFactory serviceFactory)
    { _serviceFactory = serviceFactory; }

    public override string ViewTitle
    { get { return "Home"; } }
  }
}