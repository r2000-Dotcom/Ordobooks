using Microsoft.EntityFrameworkCore;
using OrdoBooks.DataAccsess.Data;
using OrdoBooks.DataAccsess.Repository.IRepositroy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OrdoBooks.DataAccsess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _dbSet;
        public Repository(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
            _dbSet = _context.Set<T>();
            
        }
        public void Add(T item)
        {
            _dbSet.Add(item);
            
        }

        public void AddRange(IEnumerable<T> items)
        {
            _dbSet.AddRange(items);
        }

        public IEnumerable<T> GetAll()
        {
            IQueryable<T> query = _dbSet;
            return query.ToList();
        }

        public T GetById(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query = _dbSet;
            query = query.Where(filter);
            return query.FirstOrDefault();
        }

        public void Remove(T item)
        {
            _dbSet.Remove(item);
        }
    }
}
