using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotaryOfficeProject.Data;
using NotaryOfficeProject.Dtos;
using NotaryOfficeProject.Models;
using System.Data;

namespace NotaryOfficeProject.Controllers
{
    [Authorize(Roles = "User")]
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyController : ControllerBase
    {
        private readonly AppDbContext _context;
        public PropertyController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var Properties = await _context.Properties.ToListAsync();
            return Ok(Properties);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var Property = await _context.Properties
                            .SingleOrDefaultAsync(p=>p.Id==id);
            if (Property == null)
                return NotFound($"There are not any property with id={id}");
            return Ok(Property);
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(PropertyDto dto)
        {
            Property property = new()
            {
                OwnerId = dto.OwnerId,
                Governorate = dto.Governorate,
                City = dto.City,
                District = dto.District,
                BuldingNum = dto.BuldingNum,
                ApartmentNum = dto.ApartmentNum ,
                Space = dto.Space ,
                NorthernLimit = dto.NorthernLimit ,
                SouthernLimit = dto.SouthernLimit ,
                EasternLimit = dto.EasternLimit ,
                WasternLimit = dto.WasternLimit ,
            };
            await _context.AddAsync(property);
            _context.SaveChanges();
            return Ok(property);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(int id ,PropertyDto dto)
        {
            var property = await _context.Properties.SingleOrDefaultAsync(p => p.Id == id);
            if (property is null)
                return NotFound($"There are not any porperty with id ={id}");

            property.OwnerId = dto.OwnerId;
            property.Governorate = dto.Governorate;
            property.City = dto.City;
            property.District = dto.District;
            property.BuldingNum = dto.BuldingNum;
            property.ApartmentNum = dto.ApartmentNum;
            property.Space = dto.Space;
            property.NorthernLimit = dto.NorthernLimit;
            property.SouthernLimit = dto.SouthernLimit;
            property.EasternLimit = dto.EasternLimit;
            property.WasternLimit = dto.WasternLimit;

            _context.SaveChanges();
            return Ok(property);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var Property = await _context.Properties.SingleOrDefaultAsync(p => p.Id == id);
            if (Property == null)
                return NotFound($"There are not any property with id={id}");
            _context.Remove(Property);
            _context.SaveChanges();
            return Ok(Property);
        }
    }
}
