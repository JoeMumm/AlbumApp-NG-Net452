using AlbumApp.Core.Common.Core;
using FluentValidation;
using System.Collections.Generic;

namespace AlbumApp.Client.Entities
{
  public class Album: ObjectBase
  {
    int _albumId;   string _albumNumber;      string _title;        int _genre;
    string _artist;   decimal _price;         int _stockAmount;     string _image;
    
    public int AlbumId { get { return _albumId; }
      set { if (_albumId != value)
            { _albumId = value; OnPropertyChanged(() => AlbumId); } } }
    public string AlbumNumber { get { return _albumNumber; }
      set { if (_albumNumber != value)
            { _albumNumber = value; OnPropertyChanged(() => AlbumNumber); } } }
    public string Title { get { return _title; }
      set { if (_title != value)
            { _title = value; OnPropertyChanged(() => Title); } } }
    public int Genre { get { return _genre; }
      set { if (_genre != value)
            { _genre = value; OnPropertyChanged(() => Genre); } } }
    public string Artist { get { return _artist; }
      set { if (_artist != value)
            { _artist = value; OnPropertyChanged(() => Artist); } } }
    public decimal Price { get { return _price; }
      set { if (_price != value)
            { _price = value; OnPropertyChanged(() => Price); } } }
    public int StockAmount { get { return _stockAmount; }
      set { if (_stockAmount != value)
            { _stockAmount = value; OnPropertyChanged(() => StockAmount); } } }
    public string Image { get { return _image; }
      set { if (_image != value)
            { _image = value; OnPropertyChanged(() => Image); } } }

    public ICollection<Track> Tracks { get; set; }

    class AlbumValidator : AbstractValidator<Album> {
      public AlbumValidator() {
        RuleFor(obj => obj.Title).NotEmpty();
        RuleFor(obj => obj.Artist).NotEmpty();
        RuleFor(obj => obj.Genre).GreaterThan(0);
        RuleFor(obj => obj.AlbumNumber).NotEmpty();
        RuleFor(obj => obj.Price).GreaterThanOrEqualTo(0);
        RuleFor(obj => obj.StockAmount).GreaterThanOrEqualTo(0);
      } }

    protected override IValidator GetValidator()
    { return new AlbumValidator(); }
    
  }
}
