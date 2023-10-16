using Microsoft.EntityFrameworkCore;
using OrdoBooks.Model;

namespace OrdoBooks.DataAccsess.Data
{
    public class ApplicationDbContext :DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {
                
        }
      public DbSet<BookCategory> BookCategories { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            modelBuilder.Entity<BookCategory>().HasData(
                new BookCategory { CategoryId = 1, Name = "HistoryBooks", DisplayOrder = 1 },
                new BookCategory { CategoryId = 2, Name = "Information tech", DisplayOrder = 2 },
                new BookCategory { CategoryId = 3, Name = "Law books", DisplayOrder = 3 },
                new BookCategory { CategoryId = 4, Name = "Political Books", DisplayOrder = 4 }
                );
           
        }

    }
}
