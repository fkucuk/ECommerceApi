public class Email {
    public Guid Id { get; set; }
    public string Recipient { get; set; }
    public string Subject { get; set; } 
    public string Body { get; set; }
    public string? Secret { get; set; }

    public Email(Guid id, string recipient, string subject, string body) =>
    (Id, Recipient, Subject, Body) = (id, recipient, subject, body);
    

}