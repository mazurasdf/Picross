using System;
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

        [HttpGet ("picross/dashboard")]
        public IActionResult Dashboard ()
        {
            User userInDb = LoggedIn ();
            if (userInDb == null)
            {
                return RedirectToAction ("Logout", "Home");
            }
            return View ();
        }

        [HttpGet ("picross/make")]
        public IActionResult MakePuzzle ()
        {
            return View ();
        }

        [HttpPost("picross/create")]
        public IActionResult CreatePuzzle(Puzzle puzzle)
        {
            Console.WriteLine(puzzle.puzzleData);
            return RedirectToAction("Dashboard");
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