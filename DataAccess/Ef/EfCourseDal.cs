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
    public class EfCourseDal : GenericRepository<Course>, ICourseDal
    {
        public EfCourseDal(NotlyDbContext context) : base(context)
        {
        }
        public List<Course> GetCoursesByUserId(int userId)
        {
            return _context.Courses
                           .Where(c => c.UserId == userId)
                           .ToList();
        }
    }
}
