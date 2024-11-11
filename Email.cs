public class Email {
    public Guid Id { get; set; }
    public string Recipient { get; set; }
    public string Subject { get; set; } 
    public string Body { get; set; }
    public string? Secret { get; set; }

}