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
        [MinLength (5, ErrorMessage = "Puzzle must be at least 5x5")]
        [MaxLength (40, ErrorMessage = "Puzzle cannot be larger than 40x40")]
        public int xSize { get; set; }

        [Required]
        [MinLength (5, ErrorMessage = "Puzzle must be at least 5x5")]
        [MaxLength (40, ErrorMessage = "Puzzle cannot be larger than 40x40")]
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