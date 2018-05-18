using AlbumApp.Core.Common.Core;
using FluentValidation;

namespace AlbumApp.Client.Entities
{
  public class Track : ObjectBase
  {
    int _trackId;
    string _name;
    int _number;
    int _minutes;
    int _seconds;
    int _albumId;

    public int TrackId { get { return _trackId; }
      set { if (_trackId != value)
            { _trackId = value; OnPropertyChanged(() => TrackId); } } }
    public string Name { get { return _name; }
      set { if (_name != value)
            { _name = value; OnPropertyChanged(() => Name); } } }
    public int Number { get { return _number; }
      set { if (_number != value)
            { _number = value; OnPropertyChanged(() => Number); } } }
    public int Minutes { get { return _minutes; }
      set { if (_minutes != value)
            { _minutes = value; OnPropertyChanged(() => Minutes); } } }
    public int Seconds { get { return _seconds; }
      set { if (_seconds != value)
            { _seconds = value; OnPropertyChanged(() => Seconds); } } }

    public int AlbumId { get { return _albumId; } 
      set { if (_albumId != value)
            { _albumId = value; OnPropertyChanged(() => AlbumId); } } }

    public Album Album { get; set; }

    class TrackValidator : AbstractValidator<Track> {
      public TrackValidator() {
        RuleFor(obj => obj.Name).NotEmpty();
        RuleFor(obj => obj.Number).NotEmpty();
        RuleFor(obj => obj.Minutes).Must(AnyTime);
        RuleFor(obj => obj.Seconds).Must(AnyTime);

      } }

    protected override IValidator GetValidator()
    { return new TrackValidator(); }

    private static bool AnyTime(Track track, int time)
    { return (!(track.Minutes > 0) || !(track.Seconds > 0)); }
  }
}
