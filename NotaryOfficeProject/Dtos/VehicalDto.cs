using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace NotaryOfficeProject.Dtos
{
    public class VehicalDto
    {
        [StringLength(17)]
        public string Vin { get; set; } = null!;

        [StringLength(14)]
        public string OwnerId { get; set; } = null!;

        [StringLength(17)]
        public string? LicenseNum { get; set; }

        [StringLength(17)]
        public string? LicenseEndDate { get; set; }

        [StringLength(10)]
        public string Brand { get; set; } = null!;

        [StringLength(10)]
        public string Engine { get; set; } = null!;

        [StringLength(10)]
        public string Color { get; set; } = null!;

        [StringLength(10)]
        public string Modle { get; set; } = null!;
    }
}
