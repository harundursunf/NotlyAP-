using Mapster;
using Entities.Entities;
using Core.Dto; 
using Core.Dto.Core.Dto; 
using System.Linq;

namespace Businness.Mapping
{
    public static class MapsterConfig
    {
        public static void RegisterMappings()
        {
            // Comment Mapping
            TypeAdapterConfig<Comment, CommentDto>.NewConfig()
                .Map(dest => dest.UserFullName, src => src.User != null ? src.User.FullName : null)
                .Map(dest => dest.NoteTitle, src => src.Note != null ? src.Note.Title : null);
            TypeAdapterConfig<CommentDto, Comment>.NewConfig();

            // User Mapping
            TypeAdapterConfig<User, UserDto>.NewConfig();
            TypeAdapterConfig<UserDto, User>.NewConfig();

            // Like Mapping
            TypeAdapterConfig<Like, LikeDto>.NewConfig()
                .Map(dest => dest.UserFullName, src => src.User != null ? src.User.FullName : null)
                .Map(dest => dest.NoteTitle, src => src.Note != null ? src.Note.Title : null);
            TypeAdapterConfig<LikeDto, Like>.NewConfig();

            // Course Mapping
            TypeAdapterConfig<Course, CourseDto>.NewConfig()
                .Map(dest => dest.UserFullName, src => src.User != null ? src.User.FullName : null); 
            TypeAdapterConfig<CourseDto, Course>.NewConfig();

            // NoteAttachment Mapping 
            TypeAdapterConfig<NoteAttachment, NoteAttachmentDto>.NewConfig()
               .Map(dest => dest.NoteTitle, src => src.Note != null ? src.Note.Title : null);
            TypeAdapterConfig<NoteAttachmentDto, NoteAttachment>.NewConfig();

            // Note Mapping
            TypeAdapterConfig<Note, NoteDto>.NewConfig()
                .Map(dest => dest.UserFullName, src => src.User != null ? src.User.FullName : null)
                .Map(dest => dest.UserProfilePictureUrl, src => src.User != null ? src.User.ProfilePictureUrl : null)
                .Map(dest => dest.CourseName, src => src.Course != null ? src.Course.Name : null)
                .Ignore(dest => dest.ImageUrl); 
            TypeAdapterConfig<NoteDto, Note>.NewConfig();
        }
    }
}
