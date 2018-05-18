using AlbumApp.Admin.Support;
using AlbumApp.Client.Contracts;
using AlbumApp.Client.Entities;
using AlbumApp.Core.Common.Contracts;
using AlbumApp.Core.Common.Core;
using AlbumApp.Core.Common.UI.Core;
using System;
using System.Collections.Generic;

namespace AlbumApp.Admin.ViewModels
{
  public class EditAlbumViewModel : ViewModelBase
  {
    IServiceFactory _serviceFactory;
    Album _album;

    public Album Album { get { return _album; } }
    
    public EditAlbumViewModel(IServiceFactory serviceFactory, Album album)
    { _serviceFactory = serviceFactory;
      _album = new Album { AlbumId = album.AlbumId,
        AlbumNumber = album.AlbumNumber, Artist = album.Artist,
        Genre = album.Genre, Image = album.Image,
        Price = album.Price, StockAmount = album.StockAmount,
        Title = album.Title };
      _album.CleanAll();
      SaveCommand = new DelegateCommand<object>(OnSaveCommandExecute, OnSaveCommandCanExecute);
      CancelCommand = new DelegateCommand<object>(OnCancelCommandExecute); }

    public DelegateCommand<object> SaveCommand { get; private set; }
    public DelegateCommand<object> CancelCommand { get; private set; }

    public event EventHandler<AlbumEventArgs> AlbumUpdated;
    public event EventHandler CancelEditAlbum;

    protected override void AddModels(List<ObjectBase> models)
    { models.Add(Album); }

    void OnSaveCommandExecute(object arg) {
      ValidateModel();
      if (IsValid) {
        WithClient<IInventoryService>(_serviceFactory.CreateClient<IInventoryService>(),
          inventoryClient => { bool isNew = (_album.AlbumId == 0);
            // Image and Stock population to be implemented
            if (isNew) {
              _album.Image = "https://img.discogs.com/BeECgCwj8Xiz0qVfG1VXuUg1rwE=/fit-in/300x300/filters:strip_icc():format(jpeg):mode_rgb():quality(40)/discogs-images/R-6323232-1416456365-9025.jpeg.jpg";
              _album.StockAmount = 1; }
            var savedAlbum = inventoryClient.UpdateAlbum(_album);
            if (savedAlbum != null)
              if (AlbumUpdated != null)
                AlbumUpdated(this, new AlbumEventArgs(savedAlbum, isNew)); }); } }

    bool OnSaveCommandCanExecute(object arg) { return _album.IsDirty; }

    void OnCancelCommandExecute(object arg) {
      if (CancelEditAlbum != null) CancelEditAlbum(this, EventArgs.Empty);
    }
  }
}
