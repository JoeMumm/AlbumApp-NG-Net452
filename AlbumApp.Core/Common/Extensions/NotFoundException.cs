using System;

namespace AlbumApp.Core.Common.Extensions
{
  public class NotFoundException : ApplicationException
  {
    public NotFoundException(string message) : base(message) { }
    public NotFoundException(string message, Exception exception)
      : base(message, exception) { }
  }
}
