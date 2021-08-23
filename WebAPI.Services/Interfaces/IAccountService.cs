using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Data.DTO;

namespace WebAPI.Services.Interfaces
{
    public interface IAccountService
    {
        Task<bool> Register(AccountRegistration request);
        Task<string> Login(AccountLogIn request);
        Task<bool> UserExists(string phone);
    }
}
