using AlbumApp.Core.Common.Contracts;
using AlbumApp.Core.Common.UI.Core;

namespace AlbumApp.Admin.ViewModels
{
  public class OrdersViewModel : ViewModelBase
  {
    private IServiceFactory _ServiceFactory;
    
    public OrdersViewModel(IServiceFactory serviceFactory)
    {
      _ServiceFactory = serviceFactory;
    }

    public override string ViewTitle
    {
      get { return "Orders"; }
    }
  }
}