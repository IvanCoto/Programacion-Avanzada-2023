using Inventario.Application.Contracts.Identity;
using Inventario.Domain.InputModels;
using Microsoft.AspNetCore.Mvc;

namespace Inventario.UI.Controllers
{
    public class AccountController : Controller
    {
        public AccountController(IAccountService service)
        {
            _service = service;
        }
        readonly IAccountService _service;

        [HttpGet]
        public IActionResult Register()
        {
            return View(new Register());
        }

        [HttpPost]
        public async Task<IActionResult> Register(Register model)
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
    }
}
