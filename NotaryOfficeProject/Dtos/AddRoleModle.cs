using System.ComponentModel.DataAnnotations;

namespace NotaryOfficeProject.Dtos
{
    public class AddRoleModle
    {
        [Required]
        public string UserId { get; set; } = null!;

        [Required]
        public string RloeName { get; set; } = null!;
    }
}