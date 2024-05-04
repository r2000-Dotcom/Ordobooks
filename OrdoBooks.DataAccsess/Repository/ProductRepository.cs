using OrdoBooks.DataAccsess.Data;
using OrdoBooks.DataAccsess.Migrations;
using OrdoBooks.DataAccsess.Repository.IRepositroy;
using OrdoBooks.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OrdoBooks.DataAccsess.Repository
{
    public class ProductRepository : Repository<Product>,IProductRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _context = applicationDbContext;
        }

        public void save()
        {
            _context.SaveChanges();
        }

        public void Update(Product products)
        {
            _context.Update(products);
        }
    }
}
