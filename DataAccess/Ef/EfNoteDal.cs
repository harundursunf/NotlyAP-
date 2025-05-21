using DataAccess.Abstract;
using DataAccess.Context;
using DataAccess.Repositories; 
using Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Ef 
{
    public class EfNoteDal : GenericRepository<Note>, INoteDal
    {
        public EfNoteDal(NotlyDbContext context) : base(context) { }

        public override List<Note> GetAll()
        {
     
            return _dbSet
                .Include(n => n.User)   
                .Include(n => n.Course) 
                                        
                .AsNoTracking()
                .OrderByDescending(n => n.CreatedAt) 
                .ToList();
        }

        public List<Note> GetNotesByUserId(int userId)
        {
           
            return _dbSet
                .Where(n => n.UserId == userId)
                .Include(n => n.User)
                .Include(n => n.Course)
                
                .AsNoTracking()
                .OrderByDescending(n => n.CreatedAt) 
                .ToList();
        }

        public override Note GetById(int id)
        {
            return _dbSet
                .Include(n => n.User)
                .Include(n => n.Course)
             
                .AsNoTracking()
                .FirstOrDefault(n => n.Id == id);
        }
    }
}
