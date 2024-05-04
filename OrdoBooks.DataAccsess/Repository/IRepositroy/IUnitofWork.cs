
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdoBooks.DataAccsess.Repository.IRepositroy;
public interface IUnitofWork
{
     IcategotyRepository CategotyRepository { get; }
     IProductRepository ProductRepository { get; }

    void save();
}
