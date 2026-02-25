namespace Tarker.Booking.Domain.Models.Email
{
    public class SendEmailRequestModel
    {
        public string From { get; set; } = "donotreply@cbb87637-5499-48e2-ab48-a9441c4a9365.azurecomm.net";
        public string Subject { get; set; } = default!;
        public string? PlainTextContent { get; set; }
        public string? HtmlContent { get; set; }
        public List<string> To { get; set; } = new();
    }  
}
