using OrdoBooks.DataAccsess.Data;
using OrdoBooks.DataAccsess.Repository.IRepositroy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdoBooks.DataAccsess.Repository
{
    public class UnitofWork : IUnitofWork
    {
        
        public IcategotyRepository CategotyRepository { get;private set; }
        private readonly ApplicationDbContext _context;
        public UnitofWork(ApplicationDbContext db) 
        {
            _context = db;
            CategotyRepository = new CategoryRepository(db);
        }
         
        public void save()
        {
            _context.SaveChanges();
        }
    }
}
