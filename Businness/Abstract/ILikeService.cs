
using Core.Dto.Core.Dto; 
using System.Collections.Generic;

namespace Businness.Abstract
{
    public interface ILikeService
    {
     
        LikeDto Add(LikeDto likeDto);    /

        void Update(LikeDto likeDto);
        void Delete(LikeDto likeDto); 
        LikeDto GetById(int id);
        List<LikeDto> GetAll();
        List<LikeDto> GetLikesByUserId(int userId);
    }
}
