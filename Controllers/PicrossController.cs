using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Picross.Contexts;
using Picross.Models;

namespace Picross.Controllers
{
    public class PicrossController : Controller
    {
        private MyContext dbContext;

        public PicrossController (MyContext context)
        {
            dbContext = context;
        }

        public IActionResult Dashboard ()
        {
            return View ();
        }

        private User LoggedIn ()
        {
            User LogIn = dbContext.Users
                .Include (u => u.CreatedPuzzles)
                .FirstOrDefault (u => u.UserId == HttpContext.Session.GetInt32 ("UserId"));

            return LogIn;
        }
    }
}