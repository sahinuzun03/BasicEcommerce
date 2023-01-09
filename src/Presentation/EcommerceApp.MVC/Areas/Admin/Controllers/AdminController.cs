using Microsoft.AspNetCore.Mvc;

namespace EcommerceApp.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminController : Controller
    {
        
        public IActionResult Index()
        {
            return View();
        }
    }
}
