public class SendEmailRequest
{
    public string Recipient { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }

    public SendEmailRequest(string recipient, string subject, string body) =>
    (Recipient, Subject, Body) = (recipient, subject, body);

}