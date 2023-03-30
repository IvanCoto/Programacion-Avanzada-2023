using Inventario.Application.Contracts.Identity;
using Inventario.Domain.EntityModels.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
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
        }


    }
}
