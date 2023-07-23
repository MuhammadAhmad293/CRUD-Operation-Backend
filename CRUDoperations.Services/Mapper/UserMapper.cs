using CRUDoperations.DataModel.Entities;
using CRUDoperations.Dto.DTOs;
using Mapster;

namespace CRUDoperations.Services.Mapper
{
    public class UserMapper : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<UserDto, User>();
        }
    }
}
