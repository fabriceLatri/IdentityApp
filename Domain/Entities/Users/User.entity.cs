namespace Domain.Entities.Users;

public record User() : IUser
{
    public required string Id { get; init; }
    public required string Email { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public bool IsEmailConfirmed { get; init; }
}