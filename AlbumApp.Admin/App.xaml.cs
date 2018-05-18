using AlbumApp.Admin.Bootstrapper;
using AlbumApp.Core.Common.Core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace AlbumApp.Admin
{
  /// <summary>
  /// Interaction logic for App.xaml
  /// </summary>
  public partial class App : Application
  {
    public App()
    { System.Windows.FrameworkCompatibilityPreferences // allows decimal points
          .KeepTextBoxDisplaySynchronizedWithTextProperty = false; }

    protected override void OnStartup(StartupEventArgs e)
    {
      base.OnStartup(e);

      ObjectBase.Container = AutoFacLoader.Init();
    }
  }
}
