using Core.Dto; // LikedNoteInfoDto ve LikeDto için (namespace'iniz Core.Dto.Core.Dto olabilir)
using Core.Dto.Core.Dto;
using System.Collections.Generic;

namespace Businness.Abstract
{
    public interface ILikeService
    {
        /// <summary>
        /// Yeni bir beğeni ekler veya mevcutsa var olanı döndürür.
        /// Dönen LikeDto'nun UserFullName ve NoteTitle alanları dolu olmalıdır.
        /// </summary>
        LikeDto Add(LikeDto likeDto); // İstek DTO'su sadece UserId ve NoteId içerebilir

        /// <summary>
        /// Bir beğeniyi günceller (genellikle pek kullanılmaz).
        /// </summary>
        void Update(LikeDto likeDto);

        /// <summary>
        /// Bir beğeniyi kendi ID'si ile siler.
        /// </summary>
        void DeleteByLikeId(int likeId); // Metot adı daha açıklayıcı olabilir

        /// <summary>
        /// Bir kullanıcının belirli bir nota yaptığı beğeniyi siler (Unlike işlemi).
        /// </summary>
        bool DeleteByUserIdAndNoteId(int userId, int noteId);

        /// <summary>
        /// Belirli bir ID'ye sahip beğeniyi, ilişkili kullanıcı ve not bilgileriyle getirir.
        /// </summary>
        LikeDto GetById(int id);

        /// <summary>
        /// Tüm beğenileri, ilişkili kullanıcı ve not bilgileriyle getirir.
        /// </summary>
        List<LikeDto> GetAll();

        /// <summary>
        /// Belirli bir kullanıcının beğendiği notları, detaylı bilgilerle (başlık, yazar, ders, toplam beğeni) getirir.
        /// Yinelenen notları engeller (her not için bir giriş).
        /// </summary>
        List<LikedNoteInfoDto> GetLikesByUserId(int userId);
    }
}