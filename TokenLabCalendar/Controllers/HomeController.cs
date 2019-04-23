using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TokenLabCalendar.Models;
using Pomelo.EntityFrameworkCore.MySql;
using Pomelo.EntityFrameworkCore;

namespace TokenLabCalendar.Controllers
{
    public class HomeController : Controller
    {
        #region Protected Members

        protected CalendarioContext mContext;
        protected UserManager<ApplicationUser> mUserManager;
        protected SignInManager<ApplicationUser> mSignInManager;
        #endregion

        #region Constructor

        public HomeController(CalendarioContext context, 
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            mContext = context;
            mUserManager = userManager;
            mSignInManager = signInManager;
        }

        #endregion
        public IActionResult Index()
        {
            return View();
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

        [Route("create")]
        public async Task<IActionResult> CreateUserAsync()
        {
            var user = new ApplicationUser(
                UserName: "AntonioMelo",
                FirstName: "Thadeu",
                LastName: "Melo",
                Email: "thadeu2.afm@gmail.com"
            );
            var successful = await mUserManager.CreateAsync(user
                , "Pasword123456#");
            if(successful.Succeeded)
                return Content("User was created", "text/html");

            return Content("User created FAILED", "text/html");
        }
    }
}
