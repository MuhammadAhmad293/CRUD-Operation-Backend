using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Common.Enums
{
    public enum MailStatusEnum
    {

        [Display(Name = "جديد", ShortName = "New")]
        [Description("New")]
        New = 1,

        [Display(Name = "تم الإرسال", ShortName = "Sent")]
        [Description("Sent")]
        Sent = 2,

        [Display(Name = "معالجة", ShortName = "Processing")]
        [Description("Processing")]
        Processing = 3,

        [Display(Name = "فشل", ShortName = "Failed")]
        [Description("Failed")]
        Failed = 4
    }
}
