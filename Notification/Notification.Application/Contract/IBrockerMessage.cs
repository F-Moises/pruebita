using Notification.Application.Models;

namespace Notification.Application.Contract
{
    public interface IBrockerMessage
    {
        Task<EmailToSend> Consume(CancellationToken cancellationToken);
    }
}
