using OrdoBooks.Model;

namespace OrdoBooks.DataAccsess.Repository.IRepositroy;

public interface IcompanyRepository: IRepository<Company>
{
    void Update(Company company);
    void save();
}