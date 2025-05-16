using Businness.Abstract;
using Core.Dto;
using DataAccess.Abstract;
using Entities.Entities;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Businness.Concrete
{
    public class CommentManager : ICommentService
    {
        private readonly ICommentDal _commentDal;

        public CommentManager(ICommentDal commentDal)
        {
            _commentDal = commentDal;
        }

        public void Add(CommentDto commentDto)
        {
            var comment = commentDto.Adapt<Comment>();
            comment.CreatedAt = DateTime.Now;
            _commentDal.Add(comment);
        }

        public void Delete(CommentDto commentDto)
        {
            var comment = _commentDal.GetById(commentDto.Id);
            if (comment != null)
            {
                _commentDal.Delete(comment);
            }
        }

        public List<CommentDto> GetAll()
        {
            var comments = _commentDal.GetAllWithIncludes();
            var commentDtos = comments.Select(c => new CommentDto
            {
                Id = c.Id,
                Text = c.Text,
                CreatedAt = c.CreatedAt,
                UserId = c.UserId,
                UserFullName = c.User?.FullName,
                NoteId = c.NoteId,
                NoteTitle = c.Note?.Title
            }).ToList();

            return commentDtos;
        }

        public CommentDto GetById(int id)
        {
            var comment = _commentDal.GetByIdWithIncludes(id);
            if (comment == null) return null;

            return new CommentDto
            {
                Id = comment.Id,
                Text = comment.Text,
                CreatedAt = comment.CreatedAt,
                UserId = comment.UserId,
                UserFullName = comment.User?.FullName,
                NoteId = comment.NoteId,
                NoteTitle = comment.Note?.Title
            };
        }

        public void Update(CommentDto commentDto)
        {
            var comment = _commentDal.GetById(commentDto.Id);
            if (comment == null) return;

            comment.Text = commentDto.Text;
            // Güncelleme tarihi veya başka alanlar eklenebilir
            _commentDal.Update(comment);
        }
    }
}
