
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Common.Enums
{
    public enum MailTypeEnum
    {
        [Display(Name = "نسيان كلمة السر", ShortName = "Forget Password")]
        [Description("Forget Password")]
        ForgetPassword = 1,

        [Display(Name = "بريد الترحيب", ShortName = "Welcome Mail")]
        [Description("Welcome Mail")]
        WelcomeMail = 2,

        [Display(Name = "بريد التحقق", ShortName = "Verification Mail")]
        [Description("Verification Mail")]
        VerificationMail = 3
    }
}
