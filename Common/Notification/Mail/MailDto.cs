namespace Common.Notification.Mail
{
    public class MailDto
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public List<string> MailTo { get; set; }
        public List<string> MailCc { get; set; }
        public List<string> MailBcc { get; set; }
        public bool IsBodyHtml { get; set; }
        public List<string> Attachment { get; set; }
    }
}
