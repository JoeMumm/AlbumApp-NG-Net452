using AlbumApp.Admin.ViewModels;
using AlbumApp.Core.Common.UI.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using Core.Common;

namespace AlbumApp.Admin.Views
{
  /// <summary>
  /// Interaction logic for MaintainAblumsView.xaml
  /// </summary>
  public partial class MaintainAlbumsView : UserControlViewBase
  {
    public MaintainAlbumsView()
    {
      InitializeComponent();
    }

    protected override void OnWireViewModelEvents(ViewModelBase viewModel)
    {
      MaintainAlbumsViewModel vm = viewModel as MaintainAlbumsViewModel;
      if (vm != null) {
        vm.ConfirmDelete += OnConfirmDelete;
        vm.ErrorOccured += OnErrorOccured; }
    }

    protected override void OnUnwireViewModelEvents(ViewModelBase viewModel)
    {
      MaintainAlbumsViewModel vm = viewModel as MaintainAlbumsViewModel;
      if (vm != null) {
        vm.ConfirmDelete -= OnConfirmDelete;
        vm.ErrorOccured -= OnErrorOccured; }
    }

    void OnConfirmDelete(object sender, CancelEventArgs e)  {
      MessageBoxResult result = MessageBox.Show(
        "Are you sure you want to delete this album", "Confirm Delete",
        MessageBoxButton.YesNo, MessageBoxImage.Question);
      if (result == MessageBoxResult.No) e.Cancel = true; }

    void OnErrorOccured(object sender, ErrorMessageEventArgs e) {
      MessageBox.Show(e.ErrorMessage, "Error",
        MessageBoxButton.OK, MessageBoxImage.Error); }


  }
}
