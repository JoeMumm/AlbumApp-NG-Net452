﻿using System;

namespace AlbumApp.Common
{
  public class AuthorizationValidationException : ApplicationException
  {
    public AuthorizationValidationException(string message)
        : this(message, null) { }

    public AuthorizationValidationException(string message, Exception innerException)
        : base(message, innerException) { }
  }
}
