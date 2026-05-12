using System.ComponentModel.DataAnnotations;

namespace talabat.API.DTOs.IdentityDtos
{
    public class RegisterDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
