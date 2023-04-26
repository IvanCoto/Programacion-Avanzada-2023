using Inventario.Application.Contracts.Identity;
using Inventario.Application.Contracts.Recaptcha;
using Inventario.Application.Contracts.Services;
using Inventario.Domain.ComponentModels;
using Inventario.Domain.InputModels;
using Inventario.Identity.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Inventario.UI.Controllers
{
    [EnhancedAuthorize("Account")]
    public class AccountController : Controller
    {

        public AccountController
            (IAccountService service, IGoogleRecaptchaService recaptchaService, IEmailSender emailSender)
        {
            _service = service;
            _recaptchaService = recaptchaService;
            _emailSender = emailSender;
        }
        readonly IAccountService _service;
        readonly IGoogleRecaptchaService _recaptchaService;
        readonly IEmailSender _emailSender;

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Register()
        {
            var model = new RegisterInputModel
            {
                ExternalLogins = (await _service.ListExternalLogins()).ToList()
            };
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterInputModel model)
        {
            if (ModelState.IsValid)
            {
                try
                { await _service.Register(model.Email, model.Password); }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
                return RedirectToAction("Home", "Index");
            }
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login()
        {
            var model = new LoginInputModel
            {
                ExternalLogins = (await _service.ListExternalLogins()).ToList()
            };
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginInputModel model)
        {
            model.ExternalLogins = (await _service.ListExternalLogins()).ToList();



            var recaptchaResult = await _recaptchaService.VerifyToken(model.Token);

            if (recaptchaResult)
            {
                await _service.Login(model.Email, model.Password);
                return RedirectToAction("Home", "Index");
            }

            return View(model);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _service.Logout();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ExternalLogin(string provider, string returnUrl)
        {
            var redirectUrl = Url.Action("ExternalLoginCallBack", "Account",
                new { ReturnUrl = returnUrl });

            var properties =
                _service.ConfigureExternalLogin(provider, redirectUrl);

            return new ChallengeResult(provider, properties);

        }

        [AllowAnonymous]
        [Route("signin-google")]
        [Route("[controller]/action")]
        public async Task<IActionResult> ExternalLoginCallBack
            (string returnUrl, string remoteError)
        {
            if (string.IsNullOrEmpty(returnUrl))
                returnUrl = "~/";

            var model = new RegisterInputModel
            {
                ReturnUrl = returnUrl,
                ExternalLogins = (await _service.ListExternalLogins()).ToList()
            };

            if (!string.IsNullOrEmpty(remoteError))
            {
                ModelState.AddModelError
                    ("", $"Error from external provider: {remoteError}");
                return View(nameof(Register), model);
            }

            var info = await _service.GetExternalLoginInfo();
            if (info == null)
            {
                ModelState.AddModelError
                    ("", $"Error loading external information.");
                return View(nameof(Register), model);
            }

            if (!await _service.RegisterExternalLogin(info))
            {
                ModelState.AddModelError
                    ("", $"Error loading external information.");
                return View(nameof(Register), model);
            }



            return LocalRedirect(returnUrl);
        }


        [HttpGet]
        public async Task<IActionResult> ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                    await _service.ChangePassword(userId, model.CurrentPassword, model.NewPassword);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
                return RedirectToAction("Home", "Index");
            }
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPasswordAsync(ForgotPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                // Generate token
                var token = await _service.GetForgotPasswordToken(model.Email);

                if (token != null)
                {

                    // Prepare email
                    var email = new Email
                    {
                        Recipient = model.Email,
                        Subject = "Reset Password",
                        Body = $"Reset your password by clicking on the following link: https://localhost:7239/Account/ResetPassword?email={model.Email}&token={token}"
                    };

                    // Send email
                    _emailSender.Send(email);

                    ModelState.Clear();
                    model.EmailSent = true;

                    return RedirectToAction("Done");
                }


            }

            return View(model);
        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult Done()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult DonePassword()
        {
            return View();
        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(string email = null, string token = null)
        {
            if (email == null || token == null)
            {
                return View();
            }

            var model = new ResetPasswordModel { Email = email, Token = token };
            return View(model);
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
        {

            if (ModelState.IsValid)
            {
                var result = await _service.ResetPassword(model.Email, model.Token, model.NewPassword);
                if (result)
                {
                    return RedirectToAction("DonePassword");
                }
            }

            ModelState.AddModelError(string.Empty, "Error al restablecer la contraseña.");
            return View(model);
        }

    }
}
