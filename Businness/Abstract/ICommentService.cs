using Core.Dto;
using System.Collections.Generic;

namespace Businness.Abstract
{
    public interface ICommentService
    {
        void Add(CommentDto commentDto);
        void Update(CommentDto commentDto);
        void Delete(CommentDto commentDto);
        CommentDto GetById(int id);
        List<CommentDto> GetAll();
    }
}
