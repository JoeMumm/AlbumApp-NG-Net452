using AlbumApp.Admin.Support;
using AlbumApp.Client.Contracts;
using AlbumApp.Client.Entities;
using AlbumApp.Core.Common.Contracts;
using AlbumApp.Core.Common.UI.Core;
using Core.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.ServiceModel;
using System.Windows.Threading;

namespace AlbumApp.Admin.ViewModels
{
  public class MaintainAlbumsViewModel : ViewModelBase
  {
    private IServiceFactory _serviceFactory;
    bool _isBusy = true;

    public bool IsBusy {
      get { return _isBusy; }
      set {
        if (_isBusy != value)
        { _isBusy = value;
          OnPropertyChanged(() => IsBusy, false); } } }

    public override string ViewTitle
    { get { return "Maintain Albums"; } }

    public MaintainAlbumsViewModel(IServiceFactory serviceFactory) {
      _serviceFactory = serviceFactory;
      EditAlbumCommand = new DelegateCommand<AlbumViewModel>(OnEditAlbumCommand);
      AddAlbumCommand = new DelegateCommand<object>(OnAddAlbumCommand);
      DeleteAlbumCommand = new DelegateCommand<AlbumViewModel>(OnDeleteAlbumCommand);
    }

    ObservableCollection<AlbumViewModel> _albumVMs;

    public DelegateCommand<AlbumViewModel> EditAlbumCommand { get; private set;  }
    public DelegateCommand<object> AddAlbumCommand { get; private set; }
    public DelegateCommand<AlbumViewModel> DeleteAlbumCommand { get; private set; }

    public event CancelEventHandler ConfirmDelete;
    public event EventHandler<ErrorMessageEventArgs> ErrorOccured;    

    public ObservableCollection<AlbumViewModel> AlbumVMs {
      get { return _albumVMs; }
      set { if (_albumVMs != value) {
              _albumVMs = value;
              OnPropertyChanged(() => AlbumVMs, false); } } }
    
    protected override void OnViewLoaded() {
      _albumVMs = new ObservableCollection<AlbumViewModel>();
      IsBusy = true;
      Dispatcher.CurrentDispatcher.DelayInvoke("InitSomething",
                    () => { LoadData(); }, TimeSpan.FromSeconds(1.75)); }

    private void LoadData() {      
      WithClient<IInventoryService>(_serviceFactory
                        .CreateClient<IInventoryService>(),
        inventoryClient => {
          Album[] albumsArray = inventoryClient.GetAllAlbums();
          if (albumsArray != null)
              foreach (var album in albumsArray) {
              var aVM = new AlbumViewModel { AlbumId = album.AlbumId,
                AlbumNumber = album.AlbumNumber, Artist = album.Artist,
                Genre = GetGenre(album.Genre), Image = album.Image,
                Price = album.Price, StockAmount = album.StockAmount,
                Title = album.Title };
              AlbumVMs.Add(aVM); } });
      IsBusy = false; }

    EditAlbumViewModel _currentAlbumViewModel;

    public EditAlbumViewModel CurrentAlbumViewModel {
      get { return _currentAlbumViewModel; }
      set { if (_currentAlbumViewModel != value) {
              _currentAlbumViewModel = value;
              OnPropertyChanged(() => CurrentAlbumViewModel, false); } } }

    void OnAddAlbumCommand(object obj)
    {
      Album album = new Album();
      CurrentAlbumViewModel = new EditAlbumViewModel(_serviceFactory, album);
      CurrentAlbumViewModel.AlbumUpdated += CurrentAlbumViewModel_AlbumUpdated;
      CurrentAlbumViewModel.CancelEditAlbum += CurrentAlbumViewModel_CancelEditAlbum;
    }

    void OnEditAlbumCommand(AlbumViewModel albumVM) {
      if (albumVM != null) { 
        Album album = new Album { AlbumId = albumVM.AlbumId,
          AlbumNumber = albumVM.AlbumNumber, Artist = albumVM.Artist,
          Genre = GetGenreCode(albumVM.Genre), Image = albumVM.Image,
          Price = albumVM.Price, StockAmount = albumVM.StockAmount,
          Title = albumVM.Title };
        CurrentAlbumViewModel = new EditAlbumViewModel(_serviceFactory, album);
        CurrentAlbumViewModel.AlbumUpdated += CurrentAlbumViewModel_AlbumUpdated;
        CurrentAlbumViewModel.CancelEditAlbum += CurrentAlbumViewModel_CancelEditAlbum;
      } }

    void CurrentAlbumViewModel_AlbumUpdated(object sender, AlbumEventArgs e) {
      if (!e.IsNew) {
        AlbumViewModel albumVm = _albumVMs.Where(item => item.AlbumId == e.Album.AlbumId)
          .FirstOrDefault();
        if (albumVm != null) {
          albumVm.AlbumId = e.Album.AlbumId; albumVm.AlbumNumber = e.Album.AlbumNumber;
          albumVm.Title = e.Album.Title; albumVm.Artist = e.Album.Artist;
          albumVm.Genre = GetGenre(e.Album.Genre); albumVm.Image = e.Album.Image;
          albumVm.Price = e.Album.Price; albumVm.StockAmount = e.Album.StockAmount;
          } }
      else {
        AlbumViewModel albumVm = new AlbumViewModel { AlbumId = e.Album.AlbumId,
          AlbumNumber = e.Album.AlbumNumber, Artist = e.Album.Artist,
          Genre = GetGenre(e.Album.Genre), Image = e.Album.Image,
          Price = e.Album.Price, StockAmount = e.Album.StockAmount,
          Title = e.Album.Title };
        AlbumVMs.Add(albumVm); }
      CurrentAlbumViewModel = null; }

    void CurrentAlbumViewModel_CancelEditAlbum(object sender, EventArgs e)
    { CurrentAlbumViewModel = null; }

    void OnDeleteAlbumCommand(AlbumViewModel albumVM) {
      bool hasTracks = true, hasCartItems = true;
      bool isDeleteable = IsAlbumDeletable(ref hasTracks, ref hasCartItems, albumVM.AlbumId);
      if (isDeleteable) {
        CancelEventArgs args = new CancelEventArgs();
        if (ConfirmDelete != null) ConfirmDelete(this, args);
        if (!args.Cancel) {
          try {
            WithClient<IInventoryService>(_serviceFactory.CreateClient<IInventoryService>(),
              inventoryClient => {
                inventoryClient.DeleteAlbum(albumVM.AlbumId);
                _albumVMs.Remove(albumVM); });
          } catch (FaultException ex) {
            if (ErrorOccured != null) ErrorOccured(this, new ErrorMessageEventArgs(ex.Message));
          } catch (Exception ex) {
            if (ErrorOccured != null) ErrorOccured(this, new ErrorMessageEventArgs(ex.Message)); }
        } else
          if (ErrorOccured != null)
              ErrorOccured(this, new ErrorMessageEventArgs("Cannot delete this album.")); }
      else if (ErrorOccured != null) {
        if (hasTracks && hasCartItems)
          ErrorOccured(this, new ErrorMessageEventArgs("Cannot delete this album. It has tracks \n" +
            "and there are Cart Items for this album."));
        if (hasTracks && !hasCartItems)
          ErrorOccured(this, new ErrorMessageEventArgs("Cannot delete this album. It has tracks."));
        if (!hasTracks && hasCartItems)
          ErrorOccured(this, new ErrorMessageEventArgs("Cannot delete this album. There are \n" +
            "Cart Items for this album.")); } }

    bool IsAlbumDeletable(ref bool hasTracks, ref bool hasCartItems, int albumId) {
      bool _hasTracks = false, _hasCartItems = false;

      WithClient<ICartService>(_serviceFactory.CreateClient<ICartService>(),
        cartClient => { _hasCartItems = cartClient.CartItemsContainAlbum(albumId); });
      WithClient<IInventoryService>(_serviceFactory.CreateClient<IInventoryService>(),
        inventoryClient => { _hasTracks = inventoryClient.AlbumHasTracks(albumId); });

      hasTracks = _hasTracks; hasCartItems = _hasCartItems;
      return !hasTracks && !hasCartItems; }

    string GetGenre(int genreNumber) { var genre = "";
      switch (genreNumber) {
        case 1: genre = "Afro-Cuban Jazz"; break;
        case 2: genre = "Brazilian Jazz"; break;
        case 3: genre = "Classical"; break;
        case 4: genre = "Jazz"; break;
        case 5: genre = "Rock and Roll"; break;
        default: genre = ""; break; }
      return genre; }

    int GetGenreCode(string genreString) { int genre;
      switch (genreString) {
        case "Afro-Cuban Jazz": genre = 1; break;
        case "Brazilian Jazz": genre = 2; break;
        case "Classical": genre = 3; break;
        case "Jazz": genre = 4; break;
        case "Rock and Roll": genre = 5; break;
        default: genre = 0; break; }
      return genre; }        

  }
}

     //int totalCount; var sorts = new List<SortItem>();
     // var filter = new AlbumFilter { Title = "", Artist = "",
     //     AlbumNumber = "", Genre = 0, Price = -1, StockAmount = -1 };
     // WithClient<IInventoryService>(_serviceFactory
     //                   .CreateClient<IInventoryService>(),
     //   inventoryClient => {
     //     Album[] albums = inventoryClient.GetAllAlbumsPaged(out totalCount,
     //       filter, sorts, 0, 10);
     //     if (albums != null)
     //         foreach (var album in albums) {
     //         var aVM = new AlbumViewModel { AlbumId = album.AlbumId,
     //           AlbumNumber = album.AlbumNumber, Artist = album.Artist,
     //           Genre = GetGenre(album.Genre), Image = album.Image,
     //           Price = album.Price, StockAmount = album.StockAmount,
     //           Title = album.Title };
     //         Albums.Add(aVM); }
     //   }); }

