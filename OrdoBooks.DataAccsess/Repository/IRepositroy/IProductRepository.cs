using OrdoBooks.Model;

namespace OrdoBooks.DataAccsess.Repository.IRepositroy;
public interface IProductRepository : IRepository<Product>
{
    void Update(Product products);
    void save();
    
}