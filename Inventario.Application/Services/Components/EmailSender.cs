using Inventario.Application.Contracts.Services;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Inventario.Domain.ComponentModels;
using Inventario.Domain.ConfigurationModels;

namespace Inventario.Application.Services.Components
{
    public class EmailSender : IEmailSender
    {

        public EmailSender(IOptions<SmtpConfiguration> smtpConfiguration)
        {
            SmtpConfiguration = smtpConfiguration.Value;
        }
        readonly SmtpConfiguration SmtpConfiguration;

        public void Send(Email email)
        {
            var client =
                new SmtpClient
                {
                    Host = SmtpConfiguration.Server,
                    Port = SmtpConfiguration.Port,
                    EnableSsl = true,
                    UseDefaultCredentials = false,
                    Credentials =
                    new NetworkCredential
                    (SmtpConfiguration.UserName, SmtpConfiguration.Password)
                };
            //Crea el cuerpo del Correo Electronico
            var message = new MailMessage
            {
                From = new MailAddress(SmtpConfiguration.Sender),
                Subject = email.Subject,
                Body = email.Body
            };
            message.To.Add(new MailAddress(email.Recipient));
            //Envia el correo
            client.Send(message);
        }

    }
}
