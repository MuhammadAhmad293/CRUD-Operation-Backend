namespace Common.Notification.Mail
{
    public class MailSettingDto
    {
        public string SmtpServer { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string EmailAddress { get; set; }
        public int EmailSmtpPort { get; set; }
        public int SmtpTimeOut { get; set; }
    }
}
