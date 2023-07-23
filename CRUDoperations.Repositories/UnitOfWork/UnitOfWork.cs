using CRUDoperations.IRepositories.IRepository;
using CRUDoperations.IRepositories.UnitOfWork;
using CRUDoperations.Repositories.Context;
using CRUDoperations.Repositories.Repository;

namespace CRUDoperations.Repositories.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public Lazy<AppDbContext> AppDbContext { get; }
        public UnitOfWork(Lazy<AppDbContext> appDbContext) => AppDbContext = appDbContext;

        #region Main Methods Implementation
        public Task<int> CommitAsync() => AppDbContext.Value.SaveChangesAsync();

        public void Dispose()
        {

        }
        #endregion

        #region Repository Implementation
        public IUserRepository UserRepository => new UserRepository(AppDbContext);
        public IMailRepository MailRepository => new MailRepository(AppDbContext);
        #endregion

    }
}
