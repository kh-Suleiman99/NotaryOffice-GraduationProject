using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotaryOfficeProject.Data;
using NotaryOfficeProject.Dtos;
using NotaryOfficeProject.Models;
using System.Linq;

namespace NotaryOfficeProject.Controllers
{
    [Authorize(Roles ="User")]
    [Route("api/[controller]")]
    [ApiController]
    public class ContractController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly UserManager<Visitor> _userManager;

        public ContractController(AppDbContext contex, UserManager<Visitor> userManager)
        {
            _context = contex;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var contracts = await _context.Contracts.ToListAsync();
            return Ok(contracts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var contract = await _context.Contracts.SingleOrDefaultAsync(c => c.Id == id);
            if (contract is null)
                return NotFound($"There are not contract with id ={id}");
            return Ok(contract);
        }
        
        [HttpPost]
        public async Task<IActionResult> AddAsync([FromForm]ContractDto dto)
        {
            var isValid = _userManager.Users.Any(v=>v.Id==dto.CreatorId);
            if (!isValid)
                return NotFound($"There are not found ID = {dto.CreatorId}");
            isValid = _userManager.Users.Any(v => v.Id == dto.FirstPartyId);
            if (!isValid)
                return NotFound($"There are not found ID = {dto.FirstPartyId}");
            isValid = _userManager.Users.Any(v => v.Id == dto.SecondPartyId);
            if (!isValid)
                return NotFound($"There are not found ID = {dto.SecondPartyId}");

            var dataStream = new MemoryStream();
            await dto.ContractImage.CopyToAsync(dataStream);
            var contract = new Contract
            {
                ContractImage = dataStream.ToArray(),
                StartDate = dto.StartDate,
                EndDate= dto.EndDate,
                MonyAmount = dto.MonyAmount,
                Type = dto.Type == typeOfContract.rent?false:true,
                CreatorId = dto.CreatorId,
                CreateDate= DateTime.Now,
                FirstPartyId = dto.FirstPartyId,
                SecondPartyId = dto.SecondPartyId,
                PropertyId = dto.PropertyId,
                VehicalId = dto.VehicalId
            };
            _context.Add(contract);
            _context.SaveChanges();
            return Ok(contract);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateAsync(int id,[FromForm] ContractDto dto)
        {
            var contract = await _context.Contracts.SingleOrDefaultAsync(c=>c.Id==id);
            if (contract is null)
                return NotFound($"There are not a contract with id ={id}");
            var dataStream = new MemoryStream();
            await dto.ContractImage.CopyToAsync(dataStream);
            contract.ContractImage = dataStream.ToArray();
            contract.StartDate = dto.StartDate;
            contract.EndDate = dto.EndDate;
            contract.MonyAmount = dto.MonyAmount;
            contract.Type = dto.Type == typeOfContract.rent ? false : true;
            contract.CreatorId = dto.CreatorId;
            contract.CreateDate = DateTime.Now;
            contract.FirstPartyId = dto.FirstPartyId;
            contract.SecondPartyId = dto.SecondPartyId;
            contract.PropertyId = dto.PropertyId;
            contract.VehicalId = dto.VehicalId;
            _context.SaveChanges();
            return Ok(contract);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAync(int id)
        {
            var contract = await _context.Contracts.SingleOrDefaultAsync(c => c.Id == id);
            if (contract == null)
                return NotFound($"There are not a contract with id ={id}");
            _context.Remove(contract);
            _context.SaveChanges();
            return Ok(contract);
        }


        
    }
}
