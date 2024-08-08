using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using NotaryOfficeProject.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NotaryOfficeProject.Dtos
{
    public enum typeOfContract
    {
        rent = 0,
        sale = 1
    }
    public class ContractDto
    {

        public IFormFile ContractImage { get; set; } = null!;

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public decimal MonyAmount { get; set; }

        public typeOfContract Type { get; set; }

        [StringLength(14)]
        public string CreatorId { get; set; } = null!;

        //public DateTime CreateDate { get; set; }

        [StringLength(14)]
        public string FirstPartyId { get; set; } = null!;

        [StringLength(14)]
        public string SecondPartyId { get; set; } = null!;

        public int? PropertyId { get; set; }

        [StringLength(17)]
        public string? VehicalId { get; set; }
    }
}
