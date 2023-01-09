using EcommerceApp.Application.Models.DTOs;
using EcommerceApp.Application.Services.LoginService;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace EcommerceApp.MVC.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILoginService _loginService;
        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            var loggedUser = await _loginService.Login(loginDTO);

            if (loggedUser != null)
            {
                var jsonUser = JsonConvert.SerializeObject(loggedUser);

                HttpContext.Session.SetString("logedUser", jsonUser);

                var claims = new List<Claim>();

                claims.Add(new Claim(ClaimTypes.Role, loggedUser.Roles.ToString()));

                var userIdentity = new ClaimsIdentity(claims, "Login");

                ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                if(loggedUser.Roles == Domain.Enums.Roles.Admin)
                {
                    return RedirectToAction("Index", "Admin", new { area = "Admin" });
                }

                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                HttpContext.Response.Cookies.Delete(CookieAuthenticationDefaults.AuthenticationScheme);
            }

            return View(loginDTO);
        }
    }
}
