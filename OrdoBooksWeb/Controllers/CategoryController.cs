using Microsoft.AspNetCore.Mvc;
using OrdoBooks.DataAccsess.Data;
using OrdoBooks.DataAccsess.Repository.IRepositroy;
using OrdoBooks.Model;

namespace OrdoBooksWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IUnitofWork _iunitwork;

        public CategoryController(IUnitofWork dbContext)
        {
            _iunitwork = dbContext;

        }
        public IActionResult Index()
        {
            var CategotyList = _iunitwork.CategotyRepository.GetAll().OrderBy(x => x.DisplayOrder).ToList();
            return View(CategotyList);
        }
        public IActionResult CreateNewCategory()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateNewCategory(BookCategory obj)
        {
            if (obj.Name != null)
            {
                if (obj != null && obj.Name.ToLower() == obj.DisplayOrder.ToString())
                {
                    ModelState.AddModelError("Name", "Name and Display Order Can not be same ");
                }
            }


            if (ModelState.IsValid)
            {
                if (obj != null)
                {
                    _iunitwork.CategotyRepository.Add(obj);
                    _iunitwork.CategotyRepository.save();
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

            var category = _iunitwork.CategotyRepository.GetById(x => x.CategoryId == id);
            if (category == null)
            {
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
                    _iunitwork.CategotyRepository.Update(obj);
                    _iunitwork.CategotyRepository.save();
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

            var category = _iunitwork.CategotyRepository.GetById(x => x.CategoryId == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(BookCategory obj)
        {
            if (ModelState.IsValid)
            {
                if (obj != null)
                {
                    _iunitwork.CategotyRepository.Remove(obj);
                    _iunitwork.CategotyRepository.save();
                    TempData["Succsess"] = "Category Deleted Succsessfully";
                    return RedirectToAction("Index");
                }
            }
            return View();
        }
    }
}
