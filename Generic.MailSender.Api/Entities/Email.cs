namespace Generic.MailSender.Api.Entities
{
    public class Email
    {
        public string[] Destinatary { get; set; }
        public string Subject { get; set; }
        public string TemplateName { get; set; }
        public string JsonData { get; set; }
    }
}
