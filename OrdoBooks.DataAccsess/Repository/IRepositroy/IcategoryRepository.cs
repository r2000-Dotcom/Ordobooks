using OrdoBooks.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdoBooks.DataAccsess.Repository.IRepositroy
{
    public interface IcategotyRepository: IRepository<BookCategory>
    {
        void Update(BookCategory bookCategory);
        void save();
    }
}
