using CRUDoperations.DataModel.Entities;
using CRUDoperations.IRepositories.IRepository;
using CRUDoperations.Repositories.Base;
using CRUDoperations.Repositories.Context;

namespace CRUDoperations.Repositories.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(Lazy<AppDbContext> appDbContext) : base(appDbContext)
        {
        }
    }
}
