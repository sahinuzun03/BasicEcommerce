﻿using EcommerceApp.Application.Models.DTOs;
using EcommerceApp.Application.Models.VMs;
using EcommerceApp.Application.Services.AdminService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Json;

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

        public IActionResult AddManager()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddManager([FromForm]AddManagerDTO addManagerDTO)
        {

            var ApiAddManagerDTO = _adminService.GetApiAddManagerDTO(addManagerDTO);
            
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7186/");

                    var httpResponseMessage = await client.PostAsJsonAsync<ApiAddManagerDTO>("api/Manager/PostManager", ApiAddManagerDTO);

                    if (httpResponseMessage.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(ListOfManagers));
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
            }
            else
            {
                return BadRequest();
            }

            //push gözükmüyor o yüzden commit atıyorum
        }

        public async Task<IActionResult> ListOfManagers()
        {
            //var managers = await _adminService.GetManagers();
            //return View(managers);

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7186/"); //API'ın senin localinde bulunan adresini ya da server adress!!

                var responseTask = client.GetAsync("api/Manager/GetManagers");
                //Api'de bize bilgileri getirecek olan route'u yanı actionresult'ı tetikledim.
                responseTask.Wait();//Bu işlemin gerçeklşmesini bekle.

                var resultTask = responseTask.Result;

                if (responseTask.IsCompletedSuccessfully)
                {
                    var readTask = resultTask.Content.ReadAsStringAsync();
                    readTask.Wait();

                    var readData = JsonConvert.DeserializeObject<List<ListOfManagerVM>>(readTask.Result);

                    return View(readData);
                }

                else
                {
                    ViewBag.EmptyList = "List is not found";
                    return View(new List<ListOfManagerVM>());
                }
            }
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
