using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotaryOfficeProject.Data;
using NotaryOfficeProject.Dtos;
using NotaryOfficeProject.Models;

namespace NotaryOfficeProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly AppDbContext _context;
        public ServicesController(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var services = await _context.Services.ToListAsync();
            return Ok(services);
        }

        [HttpPost]
        public async Task<IActionResult> Add(ServiceDto dto)
        {
            var service = new Service
            {
                ServiceNameAr = dto.ServiceNameAr,
                ServiceNameEn= dto.ServiceNameEn
            };
            _context.Services.Add(service);
            _context.SaveChanges();
            return Ok(service);
        }
    }
}
