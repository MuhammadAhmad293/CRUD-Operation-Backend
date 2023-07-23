using CRUDoperations.DataModel.Base;

namespace CRUDoperations.DataModel.Entities
{
    public class MailStatus : BaseMultilingualTextEntity
    {
        public int MailStatusId { get; set; }
        public ICollection<Mail> Mails { get; set; }
    }
}
