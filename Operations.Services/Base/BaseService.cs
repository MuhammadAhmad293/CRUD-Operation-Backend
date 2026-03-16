using Operations.IRepositories.UnitOfWork;
using Operations.Services.Localization;
using MapsterMapper;

namespace Operations.Services.Base
{
    public class BaseService
    {
        protected IUnitOfWork UnitOfWork { get; }
        protected IMapper Mapper { get; }
        public ILocalizationService Localization { get; }
        public BaseService(IUnitOfWork unitOfWork, IMapper mapper, ILocalizationService localizationService)
        {
            UnitOfWork = unitOfWork;
            Mapper = mapper;
            Localization = localizationService;
        }
    }
}
