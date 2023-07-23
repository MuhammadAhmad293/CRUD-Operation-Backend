namespace CRUDoperations.DataModel.Base
{
    public class BaseEntity
    {
        internal BaseEntity()
        {
            IsDeleted = false;
            CreationTime = DateTime.Now;
            LastModificationTime = DateTime.Now;
        }
        public bool IsDeleted { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime LastModificationTime { get; set; }
    }
}
