using CRUDoperations.DataModel.Entities;
using CRUDoperations.IRepositories.IRepository;
using CRUDoperations.Repositories.Base;
using CRUDoperations.Repositories.Context;

namespace CRUDoperations.Repositories.Repository
{
    internal class MailRepository : BaseRepository<Mail>, IMailRepository
    {
        public MailRepository(Lazy<AppDbContext> appDbContext) : base(appDbContext)
        {
        }
    }
}
