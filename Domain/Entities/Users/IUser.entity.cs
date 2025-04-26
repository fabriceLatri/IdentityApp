namespace Domain.Entities.Users;

public interface IUser
{
  public string Id { get; }
	public string? Email { get; }
	public string FirstName { get; }
	public string LastName { get; }
	public bool IsEmailConfirmed { get; }
	public string? RefreshToken { get; }
	public long? ExpiresIn { get; }
}
