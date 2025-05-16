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
    public class EfNoteDal : GenericRepository<Note>, INoteDal
    {
        public EfNoteDal(NotlyDbContext context) : base(context)
        {
        }

        public List<Note> GetNotesByUserId(int userId)
        {
            return _context.Notes.Where(n => n.UserId == userId).ToList();
        }
    }
}
