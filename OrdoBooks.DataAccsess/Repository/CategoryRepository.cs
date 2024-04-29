using OrdoBooks.DataAccsess.Data;
using OrdoBooks.DataAccsess.Repository.IRepositroy;
using OrdoBooks.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OrdoBooks.DataAccsess.Repository;

public class CategoryRepository : Repository<BookCategory>, IcategotyRepository
{
    private readonly ApplicationDbContext _context;
    public CategoryRepository(ApplicationDbContext db):base(db)
    {
        _context = db;
    }
    public void save()
    {
        _context.SaveChanges();
    }

    public void Update(BookCategory bookCategory)
    {
        _context.BookCategories.Update(bookCategory);
    }
}
