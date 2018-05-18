using AlbumApp.Core.Common.Core;

namespace Tests.Core.Common
{
  internal class TestChild : ObjectBase
  {
    string _ChildName = string.Empty;

    public string ChildName { get { return _ChildName; }
      set { if (_ChildName == value) return;
              _ChildName = value;     OnPropertyChanged(() => ChildName); } } }
}
