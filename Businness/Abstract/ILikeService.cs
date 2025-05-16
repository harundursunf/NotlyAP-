using Core.Dto.Core.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Businness.Abstract
{
    public interface ILikeService
    {
        void Add(LikeDto likeDto);
        void Update(LikeDto likeDto);
        void Delete(LikeDto likeDto);
        LikeDto GetById(int id);
        List<LikeDto> GetAll();

        List<LikeDto> GetLikesByUserId(int userId);
    }
}
