using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OD_authorize_authenticate.Data;
using OD_authorize_authenticate.Models;

namespace OD_authorize_authenticate.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class HomeController : Controller
    { 
        public HomeController()
        {

        }

        [AllowAnonymous]
        [Route("")]
        [Route("[action]")]
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("[action]")]
        public ActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Login([FromForm] LoginModel loginModel)
        {
            string name = loginModel.Login ?? string.Empty;
            string pass = loginModel.Password ?? string.Empty;
            if (UserInfo.users.ContainsKey(name))
            {
                var claimsIdentity = new ClaimsIdentity(
                UserInfo.users[name],
                CookieAuthenticationDefaults.AuthenticationScheme);
                if(Encoding.ASCII.GetString(SHA256.Create().ComputeHash(Encoding.ASCII.GetBytes(pass))) == UserInfo.usersPasswords[name])
                {
                    await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity));
                }
                else
                {
                    ViewData["error"] = "Wrong password";
                    return View();
                }
                
            }
            else
            {
                ViewData["error"] = "Wrong username";
                return View();
            }
            return RedirectToAction("Privacy");
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return View();
        }

        [Route("[action]")]
        public IActionResult Privacy()
        {
            return View();
        }

    }
}
