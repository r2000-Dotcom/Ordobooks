using OrdoBooks.DataAccsess.Migrations;
using OrdoBooks.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdoBooks.DataAccsess.Repository.IRepositroy
{
    public interface IProductRepository : IRepository<Product>
    {
        void Update(Product products);
        void save();
        
    }
}
