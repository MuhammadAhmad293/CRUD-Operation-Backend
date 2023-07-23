using CRUDoperations.IRepositories.UnitOfWork;
using CRUDoperations.Services.Localization;
using MapsterMapper;

namespace CRUDoperations.Services.Base
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
