using Inventario.Domain.ComponentModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventario.Application.Contracts.Services
{
    public interface IEmailSender
    {
        void Send(Email email);
    }
}
