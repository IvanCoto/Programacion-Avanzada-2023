using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventario.Application.Contracts.Identity
{
    public interface IAccountService
    {
        Task Register(string email, string password);

        Task Login(string email, string password);
    }
}
