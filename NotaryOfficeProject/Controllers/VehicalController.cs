using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotaryOfficeProject.Data;
using NotaryOfficeProject.Dtos;
using NotaryOfficeProject.Models;
using System.Data;
using System.Drawing;

namespace NotaryOfficeProject.Controllers
{
    [Authorize(Roles = "User")]
    [Route("api/[controller]")]
    [ApiController]
    public class VehicalController : ControllerBase
    {
        private readonly AppDbContext _context;

        public VehicalController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var vehicals =await _context.Vehicals.ToListAsync();
            return Ok(vehicals);
        }

        [HttpGet("{vin}")]
        public async Task<IActionResult> GetByIdAsync(string vin)
        {
            var vehical = await _context.Vehicals
                .SingleOrDefaultAsync(v => v.Vin == vin);
            if (vehical == null)
                return NotFound($"There are not any vehical with Vin ={vin}");
            return Ok(vehical);
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(VehicalDto dto)
        {
            Vehical vehical = new()
            {
                Vin = dto.Vin,
                OwnerId = dto.OwnerId,
                LicenseNum = dto.LicenseNum,
                LicenseEndDate = dto.LicenseEndDate,
                Brand = dto.Brand,
                Engine = dto.Engine,
                Color = dto.Color,
                Modle = dto.Modle
            };
            await _context.AddAsync(vehical);
            _context.SaveChanges();
            return Ok(vehical);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(VehicalDto dto)
        {
            var vehical = await _context.Vehicals.
                            SingleOrDefaultAsync(v=>v.Vin== dto.Vin);
            if (vehical == null)
                return NotFound($"There are not any vehical with vin = {dto.Vin}");
            vehical.Vin = dto.Vin;
            vehical.OwnerId = dto.OwnerId;
            vehical.LicenseNum = dto.LicenseNum;
            vehical.LicenseEndDate = dto.LicenseEndDate;
            vehical.Brand = dto.Brand;
            vehical.Engine = dto.Engine;
            vehical.Color = dto.Color;
            vehical.Modle = dto.Modle;
            
            _context.SaveChanges();
            return Ok(vehical);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(string vin)
        {
            var vehical = await _context.Vehicals.SingleOrDefaultAsync(p => p.Vin == vin);
            if (vehical == null)
                return NotFound($"There are not any property with id={vin}");
            _context.Remove(vehical);
            _context.SaveChanges();
            return Ok(vehical);
        }

    }
}
