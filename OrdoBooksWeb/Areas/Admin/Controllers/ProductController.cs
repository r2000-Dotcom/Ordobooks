using BulkyBook.Models;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OrdoBooks.DataAccsess.Migrations;
using OrdoBooks.DataAccsess.Repository;
using OrdoBooks.DataAccsess.Repository.IRepositroy;
using OrdoBooks.Model;
using OrdoBooks.Model.ViewModel;

namespace OrdoBooksWeb.Areas.Admin.Controllers;
[Area("Admin")]
public class ProductController : Controller
{
   
        private readonly IUnitofWork _iunitwork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IUnitofWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
        _iunitwork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
    public IActionResult Index()
    {
        var products = _iunitwork.ProductRepository.GetAll().ToList();
        return View(products);
    }
    public IActionResult CreateNewProduct()
    {
        var categotylist = this._iunitwork.CategotyRepository.GetAll().ToList().Select(x => new SelectListItem {Text=x.Name,Value=x.CategoryId.ToString() });
        ViewBag.categotylist = categotylist;
        return View();
    }
    [HttpPost]
    public IActionResult CreateNewProduct(Product obj)
    {
      if (ModelState.IsValid)
        {
            if (obj != null)
            {
                _iunitwork.ProductRepository.Add(obj);
                _iunitwork.ProductRepository.save();
                TempData["Succsess"] = "Category Created Succsessfully";
                return RedirectToAction("Index");
            }
        }
        return View();
    }
    public IActionResult UpSert(int? id)
    {
        var Model = new ProductViewModel();


        if (id == null || id == 0)
        {
            return View();
        }
        var categotylist = this._iunitwork.CategotyRepository.GetAll().ToList().Select(x => new SelectListItem { Text = x.Name, Value = x.CategoryId.ToString() });
        Model.CategoryList = (IEnumerable<System.Web.Mvc.SelectListItem>?)categotylist;
        var product = _iunitwork.ProductRepository.GetById(x => x.Id == id);
        Model.Product = product;
        if (product == null)
        {
            return NotFound();
        }

        return View(Model);
    }
    [HttpPost]
    public IActionResult UpSert(Product obj)
    {
        if (ModelState.IsValid)
        {
            if (obj != null)
            {
                if (obj.Id>0)
                {
                    _iunitwork.ProductRepository.Update(obj);
                    _iunitwork.ProductRepository.save();
                    TempData["Succsess"] = "Category Edited Succsessfully";
                }
                else
                {
                    _iunitwork.ProductRepository.Add(obj);
                    _iunitwork.ProductRepository.save();
                    TempData["Succsess"] = "Category Added Succsessfully";
                }
                
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

        var Books = _iunitwork.ProductRepository.GetById(x => x.Id == id);
        if (Books == null)
        {
            return NotFound();
        }

        return View(Books);
    }
    [HttpPost, ActionName("Delete")]
    public IActionResult DeletePost(int? id)
    {
        var Books = _iunitwork.ProductRepository.GetById(x => x.Id == id);
        if (Books == null)
        {
            return NotFound();
        }
      _iunitwork.ProductRepository.Remove(Books);
       _iunitwork.ProductRepository.save();
        TempData["Succsess"] = "Category Deleted Succsessfully";
        return RedirectToAction("Index");
     }








}

