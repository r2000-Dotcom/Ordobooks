using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OrdoBooks.DataAccsess.Repository.IRepositroy;
using OrdoBooks.Model.ViewModel;
using OrdoBooks.Utility;

namespace OrdoBooksWeb.Areas.Admin.Controllers;
[Area("Admin")]
[Authorize(Roles = SD.Role_Admin)]
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
        return View();
    }
    
    public IActionResult UpSert(int? id)
    {
        var Model = new ProductViewModel();
        var categotylist = this._iunitwork.CategotyRepository.GetAll().ToList().Select(x => new SelectListItem { Text = x.Name, Value = x.CategoryId.ToString() });
        Model.CategoryList = categotylist;
        var product = _iunitwork.ProductRepository.GetById(x => x.Id == id);
        Model.Product = product;
        
        return View(Model);
    }
    [HttpPost]
    public IActionResult UpSert(ProductViewModel obj, IFormFile files)
    {

            if (ModelState.IsValid)
            {
            if (files != null)
            {
                var rootFilepath = _webHostEnvironment.WebRootPath;
                var finalpath = Path.Combine(rootFilepath, "Products");
                var fileName = Path.GetFileName(files.FileName);
                var imageUrl = Path.Combine(finalpath, fileName);
                if (!Directory.Exists(finalpath))
                {
                    Directory.CreateDirectory(finalpath);

                }
                using (var fileStream = new FileStream(Path.Combine(finalpath, fileName), FileMode.Create))
                {
                    files.CopyTo(fileStream);
                }
                obj.Product.ImageUrl= @"\"+ "Products" + @"\"+ fileName;
            }
            if (obj.Product.Id > 0)
                {
                    _iunitwork.ProductRepository.Update(obj.Product);
                    _iunitwork.ProductRepository.save();
                    TempData["Succsess"] = "Category Edited Succsessfully";
                }
                else
                {
                    _iunitwork.ProductRepository.Add(obj.Product);
                    _iunitwork.ProductRepository.save();
                    TempData["Succsess"] = "Category Added Succsessfully";
                }

            

                return RedirectToAction("Index");
            }
            else
            {
                var categotylist = this._iunitwork.CategotyRepository.GetAll().ToList().Select(x => new SelectListItem { Text = x.Name, Value = x.CategoryId.ToString() });
                obj.CategoryList = categotylist;
                return View(obj);
            }
    }
    

    
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

    #region ProdctApi
    [HttpGet]
    public async Task<JsonResult> ProductListData()
    {
        var products =  _iunitwork.ProductRepository.GetAll(includeProperties: "Category").ToList();
        var Count = products.Count;
        var data = new
        {
            rows = products,
            Total = products.Count,
        };


        return Json(data);
    }






    #endregion






}

