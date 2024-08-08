using System.ComponentModel.DataAnnotations;

namespace NotaryOfficeProject.Dtos
{
    public class SignInDto
    {
        [Required]
        [StringLength(50)]
        public string Email { get; set; } = null!;

        [Required]
        [StringLength(50)]
        public string Password { get; set; } = null!;
    }
}
