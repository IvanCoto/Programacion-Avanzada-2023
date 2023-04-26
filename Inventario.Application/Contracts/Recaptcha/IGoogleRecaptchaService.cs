using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventario.Application.Contracts.Recaptcha
{
    public interface IGoogleRecaptchaService
    {
        Task<bool> VerifyToken(string token);
    }
}
