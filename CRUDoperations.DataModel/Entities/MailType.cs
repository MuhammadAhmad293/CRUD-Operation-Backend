using CRUDoperations.DataModel.Base;

namespace CRUDoperations.DataModel.Entities
{
    public class MailType : BaseMultilingualTextEntity
    {
        public int MailTypeId { get; set; }
        public ICollection<Mail> Mails { get; set; }
    }
}
