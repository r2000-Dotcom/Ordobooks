using OrdoBooks.DataAccsess.Data;
using OrdoBooks.DataAccsess.Repository.IRepositroy;
using OrdoBooks.Model;

namespace OrdoBooks.DataAccsess.Repository;

public class CompanyRepository : Repository<Company>,IcompanyRepository
{
    private readonly ApplicationDbContext _context;
    public CompanyRepository(ApplicationDbContext db) : base(db)
    {
        _context = db;
    }
    public void save()
    {
        _context.SaveChanges();
    }

    public void Update(Company company)
    {
        _context.Companies.Update(company);
    }
}
