public interface IEmailService {
    Task<Email> GetEmail(Guid id);
    Task<Email> SendEmail(SendEmailRequest sendEmailRequest);
}