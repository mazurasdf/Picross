using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Picross.Models
{
    public class Puzzle
    {
        [Key]
        public int PuzzleId { get; set; }

        [Required]
        [Range(5,40)]
        public int xSize { get; set; }

        [Required]
        [Range(5,40)]
        public int ySize { get; set; }

        [Required]
        public string puzzleData { get; set; }

        [Required]
        public int UserId { get; set; }
        public User Creator { get; set; }
        public List<Like> LikesReceived { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        [Required]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}