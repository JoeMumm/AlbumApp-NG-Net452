using AlbumApp.Admin.Bootstrapper;
using AlbumApp.Core.Common.Core;
using AlbumApp.Core.Common.UI.Core;
using Autofac;

namespace AlbumApp.Admin.ViewModels
{
  public class MainViewModel : ViewModelBase
  {
    public MainViewModel() { }
     
    public DashboardViewModel DashboardViewModel {
      get { return Container.Resolve<DashboardViewModel>(); }    private set { } }
    
    public MaintainAlbumsViewModel MaintainAlbumsViewModel {
      get { return Container.Resolve<MaintainAlbumsViewModel>(); }     private set { } }

    public OrdersViewModel OrdersViewModel {
      get { return Container.Resolve<OrdersViewModel>(); }       private set { }
    }
  }
}
