using Microsoft.AspNetCore.Mvc;
using OrdoBooksWeb.Data;
using OrdoBooksWeb.Models;

namespace OrdoBooksWeb.Controllers
{
    public class CategoryController : Controller
    {
        private ApplicationDbContext _context;

        public CategoryController( ApplicationDbContext dbContext)
        {
            _context = dbContext;
            
        }




        public IActionResult Index()
        {
            var CategotyList = _context.BookCategories.OrderBy(x=>x.DisplayOrder).ToList(); 
            return View(CategotyList);
        }
        public IActionResult CreateNewCategory()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateNewCategory(BookCategory obj)
        { if(obj.Name != null)
            {
                if (obj != null && obj.Name.ToLower() == obj.DisplayOrder.ToString())
                {
                    ModelState.AddModelError("Name", "Name and Display Order Can not be same ");
                }
            }
           

            if(ModelState.IsValid) { 
            if (obj != null)
            {
                _context.BookCategories.Add(obj);
                _context.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
          return View();
        }
    }
}
