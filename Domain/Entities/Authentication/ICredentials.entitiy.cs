using System;

namespace Domain.Entities.Authentication;

public interface ICredentials
{
  public string Token { get; }
  public string RefreshToken { get; }
  public long ExpiresInMilliseconds { get; }
}
