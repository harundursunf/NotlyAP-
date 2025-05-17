// Businness/Concrete/LikeManager.cs
using Businness.Abstract;
using Core.Dto.Core.Dto;
using DataAccess.Abstract;
using Entities.Entities;
using Mapster;
using System.Collections.Generic; 
using System.Linq; 

namespace Businness.Concrete
{
    public class LikeManager : ILikeService
    {
        private readonly ILikeDal _likeDal;
    

        public LikeManager(ILikeDal likeDal )
        {
            _likeDal = likeDal;
            
        }

        public LikeDto Add(LikeDto likeDto) 
        {
        
            var likeEntity = new Like
            {
                UserId = likeDto.UserId,
                NoteId = likeDto.NoteId
                
            };

           
            _likeDal.Add(likeEntity);

          
            likeDto.Id = likeEntity.Id;

           
            return likeDto;
        }

        public void Update(LikeDto likeDto)
        {
          
            var existingLikeEntity = _likeDal.GetById(likeDto.Id);
            if (existingLikeEntity != null)
            {
               
                existingLikeEntity.UserId = likeDto.UserId; // Example, if these can change
                existingLikeEntity.NoteId = likeDto.NoteId;
                _likeDal.Update(existingLikeEntity);
            }
        }

        public void Delete(LikeDto likeDto) // Or DeleteById(int id)
        {
        
            var likeEntity = _likeDal.GetById(likeDto.Id);
            if (likeEntity != null)
            {
                _likeDal.Delete(likeEntity);
            }
        }

        public LikeDto GetById(int id)
        {
            var likeEntity = _likeDal.GetById(id);
         
            if (likeEntity == null) return null;

       
            var dto = likeEntity.Adapt<LikeDto>();
          
            return dto;
        }

        public List<LikeDto> GetAll()
        {
           
            var likeEntities = _likeDal.GetAll();
            var likeDtos = likeEntities.Adapt<List<LikeDto>>();
            return likeDtos;
        }

        public List<LikeDto> GetLikesByUserId(int userId)
        {
            var likes = _likeDal.GetLikesByUserId(userId);
            
            return likes.Adapt<List<LikeDto>>();
        }
    }
}
