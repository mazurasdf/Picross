using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
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
            ViewBag.AllPuzzles = dbContext.Puzzles
                .Include (p => p.Creator);
            return View ();
        }

        [HttpGet ("picross/make/xSize/ySize")]
        public IActionResult MakePuzzle (int xSize, int ySize)
        {
            User userInDb = LoggedIn ();
            if (userInDb == null)
            {
                return RedirectToAction ("Logout", "Home");
            }
            ViewBag.UserId = userInDb.UserId;
            ViewBag.xSize = xSize;
            ViewBag.ySize = ySize;
            return View ();
        }

        [HttpPost ("picross/make/set")]
        public IActionResult SetDimensions (int xDim, int yDim)
        {
            return RedirectToAction ("MakePuzzle", "Picross", new RouteValueDictionary
            { { "xSize", xDim }, { "ySize", yDim }
            });
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

        [HttpGet ("picross/{puzzleId}/delete")]
        public IActionResult DeletePuzzle (int puzzleId)
        {
            User userInDb = LoggedIn ();
            if (userInDb == null)
            {
                return RedirectToAction ("Logout", "Home");
            }
            Puzzle removeMe = dbContext.Puzzles
                .FirstOrDefault (p => p.PuzzleId == puzzleId);
            if (userInDb.UserId == removeMe.UserId){
                dbContext.Puzzles.Remove(removeMe);
                dbContext.SaveChanges();
            }
            return RedirectToAction ("Dashboard");
        }

        [HttpGet ("picross/{puzzleId}")]
        public IActionResult ShowPuzzle (int puzzleId)
        {
            User userInDb = LoggedIn ();
            if (userInDb == null)
            {
                return RedirectToAction ("Logout", "Home");
            }
            ViewBag.User = userInDb;
            Puzzle currentPuzzle = dbContext.Puzzles.FirstOrDefault (p => p.PuzzleId == puzzleId);
            return View ("ShowPuzzle", currentPuzzle);
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