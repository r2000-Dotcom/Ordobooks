using Microsoft.EntityFrameworkCore;
using OrdoBooks.DataAccsess.Data;
using OrdoBooks.DataAccsess.Repository.IRepositroy;
using System.Linq.Expressions;


namespace OrdoBooks.DataAccsess.Repository;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<T> _dbSet;
    public Repository(ApplicationDbContext applicationDbContext)
    {
        _context = applicationDbContext;
        _dbSet = _context.Set<T>();
        _context.Products.Include(u => u.Category).Include(u => u.CategoryId);

    }
    public void Add(T item)
    {
        _dbSet.Add(item);
        
    }

    public void AddRange(IEnumerable<T> items)
    {
        _dbSet.AddRange(items);
    }

    public IEnumerable<T> GetAll( string? includeProperties = null)
    {
        IQueryable<T> query = _dbSet;
        
        if (!string.IsNullOrEmpty(includeProperties))
        {
            foreach (var includeProp in includeProperties
                .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProp);
            }
        }
        return query.ToList();
    }

    public T GetById(Expression<Func<T, bool>> filter, string? includeProperties = null, bool tracked = false)
    {
        IQueryable<T> query;
        if (tracked)
        {
            query = _dbSet;

        }
        else
        {
            query = _dbSet.AsNoTracking();
        }

        query = query.Where(filter);
        if (!string.IsNullOrEmpty(includeProperties))
        {
            foreach (var includeProp in includeProperties
                .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProp);
            }
        }
        return query.FirstOrDefault();
    }

    public void Remove(T item)
    {
        _dbSet.Remove(item);
    }
}
