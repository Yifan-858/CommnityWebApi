using AutoMapper;
using CommnityWebApi.Data.DTO;
using CommnityWebApi.Data.Entities;

namespace CommnityWebApi.Data.Profiles
{
    public class UserProfile:Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDTO>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(org => org.UserId))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(org => org.UserName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(org => org.Email));
        }
    }
}
