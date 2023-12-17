using Api.DrivingAdapters.DTOs.Account;
using AutoMapper;
using Domain.Models.Account;

namespace Api.DrivenAdapters.Mappings.Account
{
	public class AccountProfile : Profile
    {
		public AccountProfile()
		{
			RegisterAccountProfile();
		}

        private void RegisterAccountProfile()
        {
            CreateMap<IUser, UserDto>()
                .AfterMap<SetJWTAction>();
        }
    }

    public class SetJWTAction : IMappingAction<IUser, UserDto>
    {
        private readonly JWTService _jWTService;

        public SetJWTAction(JWTService jWTService)
        {
            _jWTService = jWTService;
        }

        public void Process(IUser source, UserDto destination, ResolutionContext context)
        {
            destination.JWT = _jWTService.CreateJWT(source);
        }
    }
}

