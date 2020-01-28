using Microsoft.EntityFrameworkCore;
using Picross.Models;

namespace Picross.Contexts
{
    public class MyContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public MyContext (DbContextOptions options) : base (options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Puzzle> Puzzles { get; set; }
        public DbSet<Like> Likes { get; set; }
    }
}