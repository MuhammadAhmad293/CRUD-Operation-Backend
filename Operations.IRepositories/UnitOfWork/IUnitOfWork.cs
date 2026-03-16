using Operations.IRepositories.IRepository;

namespace Operations.IRepositories.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        #region Main Methods
        Task<int> CommitAsync();
        #endregion

        #region IRepository
        public IUserRepository UserRepository { get; }
        public IMailRepository MailRepository { get; }
        #endregion
    }
}
