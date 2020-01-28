using System.ComponentModel.DataAnnotations;

namespace Picross.Models
{
    public class LoginUser
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [DataType (DataType.Password)]
        public string Password { get; set; }
    }
}