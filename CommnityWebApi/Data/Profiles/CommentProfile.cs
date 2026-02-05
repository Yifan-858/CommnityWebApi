using AutoMapper;
using CommnityWebApi.Data.DTO;
using CommnityWebApi.Data.Entities;

namespace CommnityWebApi.Data.Profiles
{
    public class CommentProfile: Profile
    {
        public CommentProfile()
        {
            CreateMap<Comment, CommentDTO>()
                .ForMember(dest => dest.Content, opt => opt.MapFrom(origin => origin.Content))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(origin => origin.UserId))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(origin => origin.User.UserName))
                .ForMember(dest => dest.PostId, opt => opt.MapFrom(origin => origin.PostId))
                .ForMember(dest => dest.PostTitle, opt => opt.MapFrom(origin => origin.Post.Title));
        }
    }
}