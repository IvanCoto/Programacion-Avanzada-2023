using Inventario.Application.Contracts.Identity;
using Inventario.Domain.EntityModels.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Inventario.Identity.Services
{
    public class AccountService : IAccountService
    {
        public AccountService(UserManager<Usuario> userManager,
            SignInManager<Usuario> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        readonly UserManager<Usuario> _userManager;
        readonly SignInManager<Usuario> _signInManager;

        public async Task Register(string email, string password)
        {
            var usuario =
                new Usuario { UserName = email, Email = email };
            var result = await _userManager.CreateAsync(usuario);

            if (!result.Succeeded)
            {
                StringBuilder builder = new StringBuilder();
                foreach (var error in result.Errors)
                {
                    builder.AppendLine(error.Description);
                }
                throw new Exception(builder.ToString());
            }
            await _signInManager.SignInAsync(usuario, isPersistent: false);
        }



        public async Task Login(string email, string password)
        {
            var result =
                await _signInManager.PasswordSignInAsync
                    (email, password, isPersistent: false, lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                throw new Exception("Invalid user and password combination");
            }

            var usuario = await _userManager.FindByNameAsync(email);
            await _signInManager.SignInAsync(usuario, isPersistent: false);
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<IEnumerable<AuthenticationScheme>> ListExternalLogins()
        {
            return await _signInManager.GetExternalAuthenticationSchemesAsync();
        }

        public AuthenticationProperties ConfigureExternalLogin(string provider, string returnUrl)
        {
            return _signInManager.ConfigureExternalAuthenticationProperties
                (provider, returnUrl);
        }

        public async Task<ExternalLoginInfo> GetExternalLoginInfo()
        {



            return await _signInManager.GetExternalLoginInfoAsync();
        }

        public async Task<bool> RegisterExternalLogin(ExternalLoginInfo info)
        {
            var email = info.Principal.FindFirstValue(ClaimTypes.Email);
            if (string.IsNullOrEmpty(email))
            {
                return false;
            }

            var usuario = await _userManager.FindByEmailAsync(email);
            if (usuario == null)
            {
                usuario = new Usuario
                {
                    UserName = email,
                    Email = email
                };

                await _userManager.CreateAsync(usuario);
            }

            var login =
                await _userManager.FindByLoginAsync
                (info.LoginProvider, info.ProviderKey);

            if (login == null)
            {
                await _userManager.AddLoginAsync(usuario, info);
            }

            await _signInManager.SignInAsync(usuario, isPersistent: false);

            var result =
                await _signInManager.ExternalLoginSignInAsync
                (info.LoginProvider, info.ProviderKey, isPersistent: false);



            return result.Succeeded;

        }

        public async Task<bool> ChangePassword(string userId, string oldPassword, string newPassword)
        {
            var usuario = await _userManager.FindByIdAsync(userId);
            if (usuario != null)
            {
                var result = await _userManager.ChangePasswordAsync(usuario, oldPassword, newPassword);
                return result.Succeeded;
            }
            return false;
        }

        public async Task<string> GetForgotPasswordToken(string email)
        {
            var usuario = await _userManager.FindByEmailAsync(email);
            if (usuario != null)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(usuario);
                return Convert.ToBase64String(Encoding.UTF8.GetBytes(token));
            }
            return null;
        }

        public async Task<bool> ResetPassword(string email, string token, string password)
        {
            var usuario = await _userManager.FindByEmailAsync(email);

            if (usuario != null)
            {
                var decodedToken = Encoding.UTF8.GetString(Convert.FromBase64String(token));
                var result = await _userManager.ResetPasswordAsync(usuario, decodedToken, password);
                return result.Succeeded;
            }

            return false;
        }
    }
}
