using Mapster;
using Entities.Entities;
using Core.Dto;
using Core.Dto.Core.Dto;

namespace Businness.Mapping
{
    public static class MapsterConfig
    {
        public static void RegisterMappings()
        {
            // Comment Mapping
            TypeAdapterConfig<Comment, CommentDto>.NewConfig()
                .Map(dest => dest.UserFullName, src => src.User.FullName)
                .Map(dest => dest.NoteTitle, src => src.Note.Title);
            TypeAdapterConfig<CommentDto, Comment>.NewConfig();

            // User Mapping
            TypeAdapterConfig<User, UserDto>.NewConfig();
            TypeAdapterConfig<UserDto, User>.NewConfig();

            // Like Mapping
            TypeAdapterConfig<Like, LikeDto>.NewConfig()
                .Map(dest => dest.UserFullName, src => src.User.FullName)
                .Map(dest => dest.NoteTitle, src => src.Note.Title);
            TypeAdapterConfig<LikeDto, Like>.NewConfig();

            // Course Mapping
            TypeAdapterConfig<Course, CourseDto>.NewConfig()
                .Map(dest => dest.UserFullName, src => src.User.FullName);
            TypeAdapterConfig<CourseDto, Course>.NewConfig();


            TypeAdapterConfig<NoteAttachment, NoteAttachmentDto>.NewConfig()
               .Map(dest => dest.NoteTitle, src => src.Note.Title);
            TypeAdapterConfig<NoteAttachmentDto, NoteAttachment>.NewConfig();

            // Note Mapping
            TypeAdapterConfig<Note, NoteDto>.NewConfig()
                .Map(dest => dest.UserFullName, src => src.User.FullName)
                .Map(dest => dest.CourseName, src => src.Course.Name); // Örnek
            TypeAdapterConfig<NoteDto, Note>.NewConfig();

        }
    }
}
