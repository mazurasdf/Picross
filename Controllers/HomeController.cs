using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Picross.Contexts;
using Picross.Models;

namespace Picross.Controllers
{
    public class HomeController : Controller
    {
        private MyContext dbContext;
        public HomeController (MyContext context)
        {
            dbContext = context;
        }
        public IActionResult Index ()
        {
            return View ();
        }

        [HttpPost ("Register")]
        public IActionResult Register (User user)
        {
            if (ModelState.IsValid)
            {
                if (dbContext.Users.Any (u => u.UserName == user.UserName))
                {
                    ModelState.AddModelError ("UserName", "User name already in use!");
                    return View ("Index");
                }
                //register the user
                PasswordHasher<User> Hasher = new PasswordHasher<User> ();
                user.Password = Hasher.HashPassword (user, user.Password);
                dbContext.Add (user);
                dbContext.SaveChanges ();

                return RedirectToAction ("Login");
            }
            return View ("Index");
        }

        [HttpGet ("Login")]
        public IActionResult Login ()
        {
            return View ();
        }

        [HttpPost ("VerifyLogin")]
        public IActionResult VerifyLogin (LoginUser loginUser)
        {
            if (ModelState.IsValid)
            {
                //find if user with that email exists
                var userInDb = dbContext.Users.FirstOrDefault (u => u.UserName == loginUser.UserName);
                //if naw then go back to login with an error
                if (userInDb == null)
                {
                    ModelState.AddModelError ("Email", "Invalid email/password");
                    return View ("Login");
                }

                var hasher = new PasswordHasher<LoginUser> ();

                var result = hasher.VerifyHashedPassword (loginUser, userInDb.Password, loginUser.Password);

                if (result == 0)
                {
                    ModelState.AddModelError ("Email", "Invalid email/password");
                    return View ("Login");
                }
                else
                {
                    HttpContext.Session.SetInt32 ("UserId", userInDb.UserId);
                    HttpContext.Session.SetString ("UserEmail", userInDb.UserName);
                    return RedirectToAction("Dashboard", "Picross");
                }
            }

            return View ("Login");
        }

        public IActionResult Privacy ()
        {
            return View ();
        }

        [ResponseCache (Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error ()
        {
            return View (new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}