using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Bulky.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace BulkyWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> productList = _unitOfWork.Product.GetAll(includeProperties: "category");
            return View(productList);
        }
        public IActionResult Details(int ProductId)
        {
   

            ShoppingCart shoppingCart = new()
            {
                Product = _unitOfWork.Product.Get(u => u.Id == ProductId, includeProperties: "category"),
                count=1,
                ProductId=ProductId,
           
        };

                return View(shoppingCart);
         
        }

        [HttpPost]
        [Authorize]
        public IActionResult Details(ShoppingCart shoppingcart)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            shoppingcart.ApplicationUserId = userId;

            ShoppingCart carfromdb = _unitOfWork.ShoppingCart.Get(u=>u.ApplicationUserId==userId &&u.ProductId==shoppingcart.ProductId);

            if (carfromdb != null)
            {
               carfromdb.count += shoppingcart.count;
               _unitOfWork.ShoppingCart.Update(carfromdb);
            }
            else
            {
                _unitOfWork.ShoppingCart.Add(shoppingcart);
            }

            _unitOfWork.save();
            TempData["Success"] = "Cart updated successfully";
            return RedirectToAction(nameof(Index));

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
