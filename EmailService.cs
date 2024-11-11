using Microsoft.Azure.ServiceBus;
using System.Text;
using System.Text.Json;

public class EmailService : IEmailService
{
    private readonly QueueClient _queueClient;

    public EmailService(QueueClient queueClient)
    {
        _queueClient = queueClient;
    }

    public async Task<Email> GetEmail(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<Email> SendEmail(SendEmailRequest sendEmailRequest)
    {
        var emailItem = new Email(Guid.NewGuid(),
                                    sendEmailRequest.Recipient,
                                    sendEmailRequest.Subject,
                                    sendEmailRequest.Body);

        var messageBody = JsonSerializer.Serialize(sendEmailRequest);
        var message = new Message(Encoding.UTF8.GetBytes(messageBody));

        await _queueClient.SendAsync(message);

        return emailItem;
    }
}
