using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NotaryOfficeProject.Dtos
{
    public class VisitorsDto
    {
        [Required]
        [StringLength(14)]
        public string Id { get; set; } = null!;

        [Required]
        [StringLength(9)]
        public string? FactoryNum { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(11)]
        public string? Phone { get; set; }

        [Required]
        [StringLength(50)]
        public string? Email { get; set; }

        [Required]
        [StringLength(20)]
        public string? MomName { get; set; }

        [Required]
        [StringLength(50)]
        public string Password { get; set; } = null!;

        [Required]
        [StringLength(14)]
        public string? Governorate { get; set; }

        [Required]
        [StringLength(50)]
        public string? Address { get; set; }

        [Required]
        [StringLength(14)]
        public string? Nationality { get; set; }

        [Required]
        [StringLength(9)]
        public string? Religon { get; set; }
    }
}
