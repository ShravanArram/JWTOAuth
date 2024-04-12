using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;

namespace Basics.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

       
        [Authorize]
        public IActionResult Secret()
        {
            return View();
        }

        public IActionResult Authenicate()
        {
            var grandmaClaims = new List<Claim>() {
            
                new Claim(ClaimTypes.Name,"Shravan"),
                new Claim(ClaimTypes.Email,"Shravab@email.com"),
                new Claim("Grandma.Says","Very Good Boy")
            
            };

            var grandmaIndetity = new ClaimsIdentity(grandmaClaims, "Grandma Identity");

            var licenseClaims = new List<Claim>() {
                 new Claim(ClaimTypes.Name,"Shravan"),
                  new Claim("DrivingLicense","A+"),


            };

            var licenseIndetity = new ClaimsIdentity(grandmaClaims, "Government");

            var userPrincipal = new ClaimsPrincipal(new[] { grandmaIndetity,licenseIndetity });

            HttpContext.SignInAsync(userPrincipal);

            return RedirectToAction("Index");
        }

    }
}
