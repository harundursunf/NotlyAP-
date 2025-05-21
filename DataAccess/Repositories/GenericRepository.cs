
using DataAccess.Abstract;
using DataAccess.Context;
using Microsoft.EntityFrameworkCore; 
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Repositories
{
    public class GenericRepository<T> : IGenericDal<T> where T : class
    {
        protected readonly NotlyDbContext _context; 
        protected readonly DbSet<T> _dbSet;

        public GenericRepository(NotlyDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
        }
        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
            _context.SaveChanges();
        }

        public virtual List<T> GetAll()
{
    return _dbSet.ToList();
}

        public virtual T GetById(int id) 
        {
            return _dbSet.Find(id);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
            _context.SaveChanges();
        }
    }
}