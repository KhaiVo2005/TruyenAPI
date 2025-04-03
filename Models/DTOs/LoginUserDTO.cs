using System.ComponentModel.DataAnnotations;

namespace TruyenAPI.Models.DTOs
{
    public class LoginUserDTO
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }
        [Required]
        [DataType (DataType.Password)]
        public string Password { get; set; }
    }
}
