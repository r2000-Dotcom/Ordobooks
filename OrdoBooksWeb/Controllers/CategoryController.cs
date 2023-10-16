using Microsoft.AspNetCore.Mvc;
using OrdoBooks.DataAccsess.Data;
using OrdoBooks.Model;

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
                    TempData["Succsess"] = "Category Created Succsessfully";
                    return RedirectToAction("Index");
                }
            }
          return View();
        }
        public IActionResult EditCategory(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var category = _context.BookCategories.Where(x => x.CategoryId == id).FirstOrDefault();
            if (category == null) {
            return NotFound();
            }

            return View(category);
        }
        [HttpPost]
        public IActionResult EditCategory(BookCategory obj)
        {
            if (ModelState.IsValid)
            {
                if (obj != null)
                {
                    _context.BookCategories.Update(obj);
                    _context.SaveChanges();
                    TempData["Succsess"] = "Category Edited Succsessfully";
                    return RedirectToAction("Index");
                }
            }
            return View();
        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var category = _context.BookCategories.Where(x => x.CategoryId == id).FirstOrDefault();
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }
        [HttpPost,ActionName("Delete")]
        public IActionResult DeletePost(BookCategory obj)
        {
            if (ModelState.IsValid)
            {
                if (obj != null)
                {
                    _context.BookCategories.Remove(obj);
                    _context.SaveChanges();
                    TempData["Succsess"] = "Category Deleted Succsessfully";
                    return RedirectToAction("Index");
                }
            }
            return View();
        }
    }
}
