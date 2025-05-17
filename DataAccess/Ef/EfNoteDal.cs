using DataAccess.Abstract;
using DataAccess.Context;
using DataAccess.Repositories;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;

public class EfNoteDal : GenericRepository<Note>, INoteDal
{
    public EfNoteDal(NotlyDbContext context) : base(context) { }

    public override List<Note> GetAll()
    {
        return _dbSet
               .Include(note => note.User)
               .Include(note => note.Course)
               .ToList();
    }

    
    public override Note GetById(int id)
    {
        return _dbSet 
               .Include(note => note.User)   
               .Include(note => note.Course) 
               .FirstOrDefault(note => note.Id == id); 
    }
   

    public List<Note> GetNotesByUserId(int userId)
    {
        return _context.Notes
               .Where(note => note.UserId == userId)
               .Include(note => note.User)
               .Include(note => note.Course)
               .ToList();
    }
}