using Common.Notification.Mail;
using Microsoft.Extensions.Logging;
using System.Text.RegularExpressions;

namespace Common.Validator
{
    public class ValidatorHelper : IValidatorHelper
    {
        private ILogger Logger { get; }
        private MailSetting MailSetting { get; }

        public ValidatorHelper(ILogger<ValidatorHelper> logger, MailSetting mailSetting)
        {
            Logger = logger;
            MailSetting = mailSetting;
        }

        public Dictionary<string, bool> ValidateEmail(List<string> mailList)
        {
            Dictionary<string, bool> mailResult = new();
            if (mailList?.Count > default(int))
            {
                foreach (string mail in mailList)
                {
                    if (!mailResult.ContainsKey(mail))
                        mailResult.Add(mail, ValidateMailPattern(mail));
                }
            }
            return mailResult;
        }

        public bool ValidateMailPattern(string mail)
        {
            bool result = default;
            Match? match = Regex.Match(mail, MailSetting.MailValidationRegex, RegexOptions.IgnoreCase);
            if (match.Success)
                result = true;            
            else            
                Logger.LogError($"Invalid mail {mail}", "ValidateMail");            
            return result;
        }
    }
}
