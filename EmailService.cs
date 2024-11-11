
public class EmailService : IEmailService
{
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

        await Task.Delay(new Random().Next(0, 5000));

        return emailItem;
    }
}