public interface IEmailService {
    Task<Email> GetEmail(Guid id);
    Task<Email> SendEmail(Email emailItem);
}