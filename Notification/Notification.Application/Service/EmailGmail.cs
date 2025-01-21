using Notification.Application.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Notification.Application.Models;

namespace Notification.Application.Service
{
    public class EmailGmail : IMessageEmail
    {
        public void SendEmailAsync(EmailToSend emailToSend)
        {
            try
            {
                string fromEmail = "alexromerojuarez5@gmail.com";
                string fromPassword = "ckrk wpxd fkqh lrhp"; // Generada en el paso anterior

                // Crear el cliente SMTP
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587, // Puerto para envío seguro
                    Credentials = new NetworkCredential(fromEmail, fromPassword),
                    EnableSsl = true // Habilitar SSL
                };

                // Crear el mensaje
                MailMessage mail = new MailMessage(fromEmail,
                    emailToSend.To, emailToSend.Subject, emailToSend.Body);

                // Enviar el correo
                smtpClient.Send(mail);
            }
            catch (Exception ex)
            {
                // Loguear o manejar el error
                Console.WriteLine($"Error al enviar correo: {ex.Message}");
            }
        }

    }
}
