using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Picross.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [MinLength (5, ErrorMessage = "User name must be at least 5 characters")]
        public string UserName { get; set; }

        [Required]
        [MinLength (8, ErrorMessage = "Password must be at least 8 characters")]
        [DataType (DataType.Password)]
        public string Password { get; set; }

        [NotMapped]
        [Compare ("Password")]
        [DataType (DataType.Password)]
        public string Confirm { get; set; }
        public List<Puzzle> CreatedPuzzles { get; set; }
        public List<Like> LikesGiven { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        [Required]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}