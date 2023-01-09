using EcommerceApp.Application.Models.DTOs;
using EcommerceApp.Application.Services.AdminService;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceApp.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ManagerController : Controller
    {
        private readonly IAdminService _adminService;
        public ManagerController(IAdminService adminService)
        {
            _adminService = adminService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> AddManager()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddManager(AddManagerDTO addManagerDTO)
        {

            await _adminService.CreateManager(addManagerDTO);
            return RedirectToAction(nameof(ListOfManagers));

            //return View();

        }

        public async Task<IActionResult> ListOfManagers()
        {
            var managers = await _adminService.GetManagers();
            return View(managers);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateManager(Guid id)
        {
            var updateManager = await _adminService.GetManager(id);

            return View(updateManager);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateManager(UpdateManagerDTO updateManagerDTO)
        {
            if (ModelState.IsValid)
            {
                await _adminService.UpdateManager(updateManagerDTO);
                return RedirectToAction(nameof(ListOfManagers));
            }

            return View(updateManagerDTO);

        }

        public async Task<IActionResult> DeleteManager(Guid id)
        {
            await _adminService.DeleteManager(id);

            return RedirectToAction(nameof(ListOfManagers));

        }
    }
}
