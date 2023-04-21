using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventario.Domain.ConfigurationModels
{
    public class SmtpConfiguration
    {
        public string Sender { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string Server { get; set; }

        public int Port { get; set; }
    }
}
