using Businness.Abstract;
using Core.Dto; // LikedNoteInfoDto ve LikeDto için
using Core.Dto.Core.Dto;
using DataAccess.Abstract;
using Entities.Entities; // Like entity için
using Mapster; // Eğer Mapster kullanıyorsanız
using System.Collections.Generic;
using System.Linq;

namespace Businness.Concrete
{
    public class LikeManager : ILikeService
    {
        private readonly ILikeDal _likeDal;

        public LikeManager(ILikeDal likeDal)
        {
            _likeDal = likeDal;
        }

        public LikeDto Add(LikeDto likeDtoFromRequest) // Bu DTO'da sadece UserId ve NoteId dolu gelir
        {
    
            var existingLikeEntity = _likeDal.GetByUserIdAndNoteId(likeDtoFromRequest.UserId, likeDtoFromRequest.NoteId);

            if (existingLikeEntity != null)
            {
           
                var detailedExistingLike = _likeDal.GetLikesByUserId(existingLikeEntity.UserId)
                                               .FirstOrDefault(l => l.Id == existingLikeEntity.Id);
                if (detailedExistingLike != null)
                {
                    return new LikeDto
                    {
                        Id = detailedExistingLike.Id,
                        UserId = detailedExistingLike.UserId,
                        UserFullName = detailedExistingLike.User?.FullName, // Beğeniyi yapan
                        NoteId = detailedExistingLike.NoteId,
                        NoteTitle = detailedExistingLike.Note?.Title
                    };
                }
           
                return existingLikeEntity.Adapt<LikeDto>();
            }

            // 2. Beğeni yoksa, yeni beğeni oluştur
            var newLikeEntity = new Like
            {
                UserId = likeDtoFromRequest.UserId,
                NoteId = likeDtoFromRequest.NoteId
            };

            _likeDal.Add(newLikeEntity); // Bu işlem newLikeEntity.Id'yi doldurur (GenericRepository'deki SaveChanges sayesinde)

            // Yeni eklenen beğeniyi zenginleştirip döndür
            // EfLikeDal.GetLikesByUserId, User ve Note bilgilerini Include ile getiriyordu.
            var addedLikeWithDetails = _likeDal.GetLikesByUserId(newLikeEntity.UserId)
                                           .FirstOrDefault(l => l.Id == newLikeEntity.Id);

            if (addedLikeWithDetails != null) // Normalde null olmamalı
            {
                return new LikeDto
                {
                    Id = addedLikeWithDetails.Id,
                    UserId = addedLikeWithDetails.UserId,
                    UserFullName = addedLikeWithDetails.User?.FullName,
                    NoteId = addedLikeWithDetails.NoteId,
                    NoteTitle = addedLikeWithDetails.Note?.Title
                };
            }
      
            return newLikeEntity.Adapt<LikeDto>();
        }

        public void Update(LikeDto likeDto)
        {
            var existingLikeEntity = _likeDal.GetById(likeDto.Id);
            if (existingLikeEntity != null)
            {
                // Gelen DTO'daki verilerle entity'yi güncelle (Mapster)
                existingLikeEntity.UserId = likeDto.UserId; 
                existingLikeEntity.NoteId = likeDto.NoteId;
                _likeDal.Update(existingLikeEntity);
            }
        }

        public void DeleteByLikeId(int likeId)
        {
            var likeEntity = _likeDal.GetById(likeId);
            if (likeEntity != null)
            {
                _likeDal.Delete(likeEntity);
            }
        }

        public bool DeleteByUserIdAndNoteId(int userId, int noteId)
        {
            var likeEntity = _likeDal.GetByUserIdAndNoteId(userId, noteId);
            if (likeEntity != null)
            {
                _likeDal.Delete(likeEntity);
                return true;
            }
            return false;
        }

        public LikeDto GetById(int id)
        {
           
            var likeEntity = _likeDal.GetById(id); 
            if (likeEntity == null) return null;

            return new LikeDto
            {
                Id = likeEntity.Id,
                UserId = likeEntity.UserId,
                UserFullName = likeEntity.User?.FullName,
                NoteId = likeEntity.NoteId,
                NoteTitle = likeEntity.Note?.Title
            };
        }

        public List<LikeDto> GetAll()
        {
            
            var likeEntities = _likeDal.GetAll(); // Bu, detayları (User, Note) İÇERMELİ.
            return likeEntities.Select(le => new LikeDto
            {
                Id = le.Id,
                UserId = le.UserId,
                UserFullName = le.User?.FullName,
                NoteId = le.NoteId,
                NoteTitle = le.Note?.Title
            }).ToList();
        }

        public List<LikedNoteInfoDto> GetLikesByUserId(int userId)
        {
            // EfLikeDal.GetLikesByUserId metodu zaten Note, Note.User, Note.Course bilgilerini Include ile getiriyor.
            var likeEntitiesFromDal = _likeDal.GetLikesByUserId(userId);

            if (likeEntitiesFromDal == null || !likeEntitiesFromDal.Any())
            {
                return new List<LikedNoteInfoDto>();
            }

           
            var distinctLikesPerNote = likeEntitiesFromDal
                .GroupBy(l => l.NoteId)
                .Select(g => g.OrderByDescending(l => l.Id).First()) // Her not için en son/en yüksek ID'li beğeniyi al
                .ToList();

          
            var resultDtoList = new List<LikedNoteInfoDto>();
            foreach (var likeEntity in distinctLikesPerNote)
            {
                
                if (likeEntity.Note == null) continue;

                resultDtoList.Add(new LikedNoteInfoDto
                {
                    LikeId = likeEntity.Id, // Beğeni kaydının kendi ID'si
                    NoteId = likeEntity.NoteId,
                    NoteTitle = likeEntity.Note.Title,
                    NoteAuthorFullName = likeEntity.Note.User?.FullName ?? "Bilinmeyen Yazar",
                    NoteCourseName = likeEntity.Note.Course?.Name ?? "Bilinmeyen Ders",
                    TotalLikesForNote = _likeDal.CountLikesForNote(likeEntity.NoteId) // Her not için toplam beğeni sayısı
                });
            }
            return resultDtoList;
        }
    }
}