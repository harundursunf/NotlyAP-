// Core.Dto/NoteDto.cs
using System;
using System.ComponentModel.DataAnnotations; // Data Annotations için (gerekirse)

namespace Core.Dto
{
    public class NoteDto
    {
        public int Id { get; set; }

       
        [Required(ErrorMessage = "Başlık alanı zorunludur.")]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "Başlık en az 3, en fazla 200 karakter olmalıdır.")]
        public string Title { get; set; }

        public string? Content { get; set; } 

       
        public DateTime CreatedAt { get; set; }

      
        public int UserId { get; set; }

     
        public string? UserFullName { get; set; }

       
        public string? UserProfilePictureUrl { get; set; }

       
        [Required(ErrorMessage = "Kurs ID zorunludur.")]
        [Range(1, int.MaxValue, ErrorMessage = "Geçerli bir Kurs ID girilmelidir.")]
        public int CourseId { get; set; }

        public string? CourseName { get; set; } 

       
        public int LikesCount { get; set; }
        public bool IsLikedByCurrentUser { get; set; }
        public int? CurrentUserLikeId { get; set; }

      
        public string? ImageUrl { get; set; }
    }
}
