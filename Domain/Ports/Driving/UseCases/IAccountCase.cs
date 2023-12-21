using System;
using Domain.Models.Account;
using Domain.Ports.Driving.DTOs.Account;

namespace Domain.Ports.Driving.UseCases
{
    public interface IAccountCase
    {
        Task<IUserDto> ExecuteLogin(string email, string password);

        Task<object> ExecuteRegister(string firstname, string lastname, string email, string password);

        Task<IUser> ExecuteRefreshUserToken(string emailClaim);
    }
}

