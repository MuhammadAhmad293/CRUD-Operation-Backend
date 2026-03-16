using Operations.DataModel.Entities;
using Operations.Dto.DTOs;
using Mapster;

namespace Operations.Services.Mapper
{
    public class UserMapper : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<UserDto, User>();
        }
    }
}
