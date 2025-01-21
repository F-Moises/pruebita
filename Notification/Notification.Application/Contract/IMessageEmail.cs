using Notification.Application.Models;

namespace Notification.Application.Contract
{
    public interface IMessageEmail
    {
        void SendEmailAsync(EmailToSend emailToSend);
    }
}
