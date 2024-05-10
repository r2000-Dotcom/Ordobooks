using Microsoft.AspNetCore.Mvc;
using OrdoBooks.DataAccsess.Repository;
using OrdoBooks.DataAccsess.Repository.IRepositroy;
using OrdoBooks.Model;
using System.Diagnostics;

namespace OrdoBooksWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitofWork unitofWork;

        public HomeController(ILogger<HomeController> logger,IUnitofWork unitof)
        {
            _logger = logger;
            unitofWork = unitof;
        }

        public IActionResult Index()
        {
            var products = unitofWork.ProductRepository.GetAll(includeProperties: "Category").ToList();
            return View(products);
        }
        //public IActionResult Details(int productId)
        //{
        //    ShoppingCart cart = new()
        //    {
        //        Product = unitofWork.ProductRepository.Get(u => u.Id == productId),
        //        Count = 1,
        //        ProductId = productId
        //    };
        //    return View(cart);
        //}

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}