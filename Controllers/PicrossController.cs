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
            ViewBag.MyUser = dbContext.Users
                .Include (u => u.CreatedPuzzles)
                .FirstOrDefault (u => u.UserId == userInDb.UserId);
            ViewBag.AllPuzzles = dbContext.Puzzles.ToList();
            return View ();
        }

        [HttpGet ("picross/make")]
        public IActionResult MakePuzzle ()
        {
            User userInDb = LoggedIn ();
            if (userInDb == null)
            {
                return RedirectToAction ("Logout", "Home");
            }
            ViewBag.UserId = userInDb.UserId;
            return View ();
        }

        [HttpPost ("picross/create")]
        public IActionResult CreatePuzzle (Puzzle puzzle)
        {
            User userInDb = LoggedIn ();
            if (userInDb == null)
            {
                return RedirectToAction ("Logout", "Home");
            }
            if (ModelState.IsValid)
            {
                Console.WriteLine ($"Here is your puzzle data: {puzzle.puzzleData}");
                dbContext.Puzzles.Add (puzzle);
                dbContext.SaveChanges ();
                return RedirectToAction ("Dashboard");
            }
            else
            {
                Console.WriteLine ("invalid puzzle??????");
                return View ("MakePuzzle");
            }
        }

        [HttpGet("picross/{puzzleId}")]
        public IActionResult ShowPuzzle(int puzzleId)
        {
            User userInDb = LoggedIn ();
            if (userInDb == null)
            {
                return RedirectToAction ("Logout", "Home");
            }
            ViewBag.User = userInDb;
            ViewBag.Puzzle = dbContext.Puzzles.FirstOrDefault(p => p.PuzzleId == puzzleId);
            return View();
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