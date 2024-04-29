using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OrdoBooks.DataAccsess.Repository.IRepositroy
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(Expression<Func<T,bool>> filter);
        void Add(T item);
        void AddRange(IEnumerable<T> items);
        void Remove(T item);
      

    }
}
