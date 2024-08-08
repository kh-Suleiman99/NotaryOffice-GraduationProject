using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NotaryOfficeProject.Dtos
{
    public class PropertyDto
    {

        [StringLength(14)]
        public string OwnerId { get; set; } = null!;

        [StringLength(14)]
        public string Governorate { get; set; } = null!;

        [StringLength(20)]
        public string City { get; set; } = null!;

        [StringLength(20)]
        public string District { get; set; } = null!;

        public int? BuldingNum { get; set; }

        public int? ApartmentNum { get; set; }
        public decimal Space { get; set; }

        [StringLength(30)]
        public string NorthernLimit { get; set; } = null!;

        [StringLength(30)]
        public string SouthernLimit { get; set; } = null!;

        [StringLength(30)]
        public string EasternLimit { get; set; } = null!;

        [StringLength(30)]
        public string WasternLimit { get; set; } = null!;

    }
}
