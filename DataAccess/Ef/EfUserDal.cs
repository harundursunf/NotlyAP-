using DataAccess.Abstract;
using DataAccess.Context;
using DataAccess.Repositories; // GenericRepository'nin bulunduğu namespace
using Entities.Entities;
using Microsoft.EntityFrameworkCore; // FirstOrDefaultAsync gibi metodlar için
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Ef
{
    public class EfUserDal : GenericRepository<User>, IUserDal
    {
        // NotlyDbContext'e base class (GenericRepository) üzerinden eriştiğinizi varsayıyorum.
        // Eğer GenericRepository'de context'e 'Context' veya '_context' gibi bir protected property ile erişiliyorsa, onu kullanın.
        // Ya da doğrudan NotlyDbContext'i burada da tutabilirsiniz.
        private readonly NotlyDbContext _dbContext; // GenericRepository'nin context'i nasıl yönettiğine bağlı

        public EfUserDal(NotlyDbContext context) : base(context)
        {
            _dbContext = context; // Veya base(context) yeterliyse bu satıra gerek yok.
        }

        public User GetByFilter(Expression<Func<User, bool>> filter)
        {
            
            return _dbContext.Set<User>().FirstOrDefault(filter);

            
        }
    }
}