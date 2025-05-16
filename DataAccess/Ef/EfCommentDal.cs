using DataAccess.Abstract;
using DataAccess.Context;
using DataAccess.Repositories;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Ef
{
    public class EfCommentDal : GenericRepository<Comment>, ICommentDal
    {
        private readonly NotlyDbContext _context;

        public EfCommentDal(NotlyDbContext context) : base(context)
        {
            _context = context;
        }

        public Comment GetByIdWithIncludes(int id)
        {
            return _context.Comments
                .Include(c => c.User)
                .Include(c => c.Note)
                .FirstOrDefault(c => c.Id == id);
        }

        public List<Comment> GetAllWithIncludes()
        {
            return _context.Comments
                .Include(c => c.User)
                .Include(c => c.Note)
                .ToList();
        }
    }
}
