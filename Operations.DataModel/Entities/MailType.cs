using Operations.DataModel.Base;

namespace Operations.DataModel.Entities
{
    public class MailType : BaseMultilingualTextEntity
    {
        public int MailTypeId { get; set; }
        public ICollection<Mail> Mails { get; set; }
    }
}
