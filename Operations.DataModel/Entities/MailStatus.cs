using Operations.DataModel.Base;

namespace Operations.DataModel.Entities
{
    public class MailStatus : BaseMultilingualTextEntity
    {
        public int MailStatusId { get; set; }
        public ICollection<Mail> Mails { get; set; }
    }
}
