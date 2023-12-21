namespace Domain;

public interface IAccountMapperPort<IUser, IUserDto>
{
  IUserDto MapTo(IUser source, string token);

  IUser MapFrom(IUserDto destination);
}
