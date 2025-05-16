using Businness.Abstract;
using Core.Dto;
using Core.Dto.Core.Dto;
using DataAccess.Abstract;
using Entities.Entities;
using Mapster;

namespace Businness.Concrete
{
    public class LikeManager : ILikeService
    {
        private readonly ILikeDal _likeDal;

        public LikeManager(ILikeDal likeDal)
        {
            _likeDal = likeDal;
        }

        public void Add(LikeDto likeDto)
        {
            var like = likeDto.Adapt<Like>();
            _likeDal.Add(like);
        }

        public void Update(LikeDto likeDto)
        {
            var like = _likeDal.GetById(likeDto.Id);
            if (like != null)
            {
                likeDto.Adapt(like);
                _likeDal.Update(like);
            }
        }

        public void Delete(LikeDto likeDto)
        {
            var like = _likeDal.GetById(likeDto.Id);
            if (like != null)
            {
                _likeDal.Delete(like);
            }
        }

        public LikeDto GetById(int id)
        {
            var like = _likeDal.GetById(id);
            return like?.Adapt<LikeDto>();
        }

        public List<LikeDto> GetAll()
        {
            return _likeDal.GetAll()
                           .Adapt<List<LikeDto>>();
        }
        public List<LikeDto> GetLikesByUserId(int userId)
        {
            var likes = _likeDal.GetLikesByUserId(userId);
            return likes.Adapt<List<LikeDto>>();
        }
    }

}
