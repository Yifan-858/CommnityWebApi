using AutoMapper;
using CommnityWebApi.Data.DTO;
using CommnityWebApi.Data.Entities;

namespace CommnityWebApi.Data.Profiles
{
    public class PostProfile: Profile
    {
        public PostProfile()
        {
            //Dest = DTO, Orig = Post
            CreateMap<Post, PostDTO>()
                .ForMember(destination => destination.Title, opt => opt.MapFrom(origin => origin.Title))
                .ForMember(dest => dest.Text, opt => opt.MapFrom(origin => origin.Text))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(origin => origin.Category))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(origin => origin.UserId))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(origin => origin.User.UserName));
        }
    }
}
