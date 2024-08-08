using System.ComponentModel.DataAnnotations;

namespace NotaryOfficeProject.Dtos
{
    public class ServiceDto
    {
        [Required]
        public string ServiceNameEn { get; set; } = null!;

        [Required]
        public string ServiceNameAr { get; set; } = null!;
    }
}
