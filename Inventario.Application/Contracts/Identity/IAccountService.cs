using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Inventario.Application.Contracts.Identity
{
    public interface IAccountService
    {
        Task Register(string email, string password);

        Task Login(string email, string password);
        Task Logout();

        Task<IEnumerable<AuthenticationScheme>> ListExternalLogins();

        AuthenticationProperties ConfigureExternalLogin(string provider, string returnUrl);

        Task<ExternalLoginInfo> GetExternalLoginInfo();

        Task<bool> RegisterExternalLogin(ExternalLoginInfo info);

        Task<bool> ChangePassword
            (string userId, string oldPassword, string newPassword);
        Task<string> GetForgotPasswordToken(string email);
        Task<bool> ResetPassword(string email, string token, string newPassword);
    }
}
