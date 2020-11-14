using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OD_authorize_authenticate.Data;

namespace OD_authorize_authenticate.Controllers
{

    [Route("[controller]")]
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        public AdminController()
        {

        }

        // GET: Admin
        [HttpGet]
        [Route("")]
        [Route("[action]")]
        public ActionResult Main()
        {
            return View();
        }
    }
}
