using OrdoBooks.DataAccsess.Data;
using OrdoBooks.DataAccsess.Repository.IRepositroy;

namespace OrdoBooks.DataAccsess.Repository;

public class UnitofWork : IUnitofWork
{
   public IcategotyRepository CategotyRepository { get;private set; }
   public IProductRepository ProductRepository { get; private set; }
   public IcompanyRepository CompanyRepository { get; private set; }


    private readonly ApplicationDbContext _context;
    public UnitofWork(ApplicationDbContext db) 
    {
        _context = db;
        CategotyRepository = new CategoryRepository(db);
        ProductRepository = new ProductRepository(db);
        CompanyRepository = new CompanyRepository(db);
    }
     
    public void save()
    {
        _context.SaveChanges();
    }
}
