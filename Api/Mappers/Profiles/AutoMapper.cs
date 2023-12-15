using System;
using Api.DTOs.Account;
using Api.Models;
using AutoMapper;

namespace Api.Mappers.Profiles
{
	public class AutoMapper : Profile
    {
		public AutoMapper()
		{
			AccountProfile();
		}

        private void AccountProfile()
        {
            CreateMap<User, UserDto>()
                .AfterMap<SetJWTAction>();
        }
    }

    public class SetJWTAction : IMappingAction<User, UserDto>
    {
        private readonly JWTService _jWTService;

        public SetJWTAction(JWTService jWTService)
        {
            _jWTService = jWTService;
        }

        public void Process(User source, UserDto destination, ResolutionContext context)
        {
            destination.JWT = _jWTService.CreateJWT(source);
        }
    }
}

