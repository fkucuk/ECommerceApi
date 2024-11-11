public class EmailItemDTO
{
    public Guid? Id { get; set; }
    public string Recipient { get; set; }
    public string Subject { get; set; } 
    public string Body { get; set; }
    public EmailItemDTO(Email emailItem) =>
    (Id, Recipient, Subject, Body) = (emailItem.Id, emailItem.Recipient, emailItem.Subject, emailItem.Body);

}