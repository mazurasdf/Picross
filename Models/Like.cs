using System.ComponentModel.DataAnnotations;

namespace Picross.Models
{
    public class Like
    {
        [Key]
        public int LikeId { get; set; }

        [Required]
        public int PuzzleId { get; set; }

        [Required]
        public int UserId { get; set; }
        public Puzzle LikedPuzzle { get; set; }
        public User LikedByUser { get; set; }
    }
}