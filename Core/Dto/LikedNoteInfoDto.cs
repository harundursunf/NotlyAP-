// Core/Dto/LikedNoteInfoDto.cs (Yeni dosya veya mevcut bir DTO dosyasına eklenebilir)
namespace Core.Dto
{
    public class LikedNoteInfoDto
    {
        public int LikeId { get; set; }         // Beğeni kaydının kendi ID'si (React key için ideal)
        public int NoteId { get; set; }         // Beğenilen notun ID'si
        public string NoteTitle { get; set; }    // Beğenilen notun başlığı
        public string NoteAuthorFullName { get; set; } // Notu yazan kişinin tam adı (Note.User.FullName gibi)
        public string NoteCourseName { get; set; } // Notun ait olduğu dersin adı (Note.Course.Name gibi)
        public int TotalLikesForNote { get; set; } // Notun toplam beğeni sayısı
        // public DateTime LikedAt { get; set; } // Opsiyonel: Kullanıcının bu notu ne zaman beğendiği
    }
}