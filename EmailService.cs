
public class EmailService : IEmailService
{
    public async Task<Email> GetEmail(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<Email> SendEmail(Email emailItem)
    {
        emailItem.Id = Guid.NewGuid();

        await Task.Delay(new Random().Next(0, 5000));

        return emailItem;
    }
}