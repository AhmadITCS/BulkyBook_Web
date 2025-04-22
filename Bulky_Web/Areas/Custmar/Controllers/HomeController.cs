using System.Diagnostics;
using BulkyBook.Models1;
using BulkyBookDataAccess.Repository.UintOfWork;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBook_Web.Areas.Custmar.Controllers
{
    [Area("Custmar")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUintOfWork _uintOfWork;
        public HomeController(ILogger<HomeController> logger ,IUintOfWork uintOfWork)
        {
            _logger = logger;
            _uintOfWork = uintOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> products = _uintOfWork.Product.GetAll(includeprop: "Category");
            return View(products);
        }
        public IActionResult Detils(int id)
        {
            Product products = _uintOfWork.Product.Get(u => u.Id==id,includeprop: "Category");
            return View(products);
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
