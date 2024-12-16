using System.ComponentModel.DataAnnotations;

namespace WikiSound.Shared.Auth
{
    public class AuthRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "The field Password can't be lower than 6")]
        [MaxLength(12, ErrorMessage = "The field Password can't be greater than 12")]
        public string Password { get; set; }
    }
}
