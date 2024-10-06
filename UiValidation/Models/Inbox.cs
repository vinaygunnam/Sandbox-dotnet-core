namespace UiValidation.Models
{
    public class Inbox
    {
        public string Sender { get; set; }
        public string Recipient { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public DateTime? Timestamp { get; set; }
    }
}