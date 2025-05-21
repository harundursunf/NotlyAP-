using DataAccess.Abstract;
using DataAccess.Context; 
using DataAccess.Repositories;

using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Ef 
{
    public class EfLikeDal : GenericRepository<Like>, ILikeDal
    {
        
        public EfLikeDal(NotlyDbContext context) : base(context)
        {
        }

        public List<Like> GetLikesByUserId(int userId)
        {
            
            return _context.Likes
                           .Where(l => l.UserId == userId)
                           .Include(l => l.Note)                
                               .ThenInclude(n => n.User)      
                           .Include(l => l.Note.Course)         
                                                                
                           .ToList();
        }

        
        public Like GetByUserIdAndNoteId(int userId, int noteId)
        {
           
            return _dbSet.FirstOrDefault(l => l.UserId == userId && l.NoteId == noteId);
        }
        public int CountLikesForNote(int noteId) 
        {
            return _dbSet.Count(l => l.NoteId == noteId);
        }
    }
}