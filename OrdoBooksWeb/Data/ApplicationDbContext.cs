using Microsoft.EntityFrameworkCore;
using OrdoBooksWeb.Models;

namespace OrdoBooksWeb.Data
{
    public class ApplicationDbContext :DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {
                
        }
      public DbSet<BookCategory> BookCategories { get; set; }


    }
}
