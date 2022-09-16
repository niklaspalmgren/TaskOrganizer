using AutoMapper;
using TaskOrganizer.Shared;

namespace TaskOrganizer.Api.Profiles;

public class UsersProfile : Profile
{
    public UsersProfile()
    {
        CreateMap<Entities.User, UserDto>();

        // Could create separate dto for read/create/update etc.. but for simplicity sake.
        CreateMap<UserDto, Entities.User>().ForMember(x => x.Id, opt => opt.Ignore());
    }
}