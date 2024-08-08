using System.ComponentModel.DataAnnotations;

namespace NotaryOfficeProject.Models
{
    public class Service
    {
        public int Id { get; set; }

        [Required]
        public string ServiceNameEn { get; set; } = null!;

        [Required]
        public string ServiceNameAr { get; set; } = null!;

    }
}
