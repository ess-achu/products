using InWebApp.Models;
using InWebApp.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace InWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DbServices _dbServices;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _dbServices = new DbServices();
        }

        public IActionResult Index(string Search)
        {
            List<ProductModel> products = new List<ProductModel>();
            products = string.IsNullOrEmpty(Search) ? _dbServices.GetProducts() : _dbServices.GetFilteredProducts(Search);
            return View(products);
        }

        public IActionResult AddProduct()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddProduct(ProductModel productModel)
        {
            try
            {
                _dbServices.AddProduct(productModel);
            }catch (Exception ex)
            {
                
            }
            return RedirectToAction("Index");
        }

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
