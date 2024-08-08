using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NotaryOfficeProject.Data;
using NotaryOfficeProject.Dtos;
using NotaryOfficeProject.Helpers;
using NotaryOfficeProject.Models;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NotaryOfficeProject.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class VisitorsController : ControllerBase
    {
        
        private readonly UserManager<Visitor> _userManager;

        private readonly RoleManager<IdentityRole> _roleManager;

        private readonly JWT _jwt;

        public VisitorsController(UserManager<Visitor> userManager, IOptions<JWT> jwt, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _jwt = jwt.Value;
            _roleManager = roleManager;
        }




        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var visitors = await _userManager.Users
                .Select(v => new VisitorsDto
                {
                    Id = v.Id,
                    FactoryNum = v.FactoryNum,
                    Name = v.Name,
                    Phone = v.PhoneNumber,
                    Email = v.Email,
                    MomName = v.MomName,
                    Governorate = v.Governorate,
                    Address = v.Address,
                    Nationality = v.Nationality,
                    Religon = v.Religon
                })
                .ToListAsync();
            return Ok(visitors);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(string id)
        {
            var visitor = await _userManager.FindByIdAsync(id);
            if (visitor is null)
                return NotFound($"There are not any user with id = {id}");

            var dto = new VisitorsDto
            {
                Id = visitor.Id,
                FactoryNum = visitor.FactoryNum,
                Name = visitor.Name,
                Phone = visitor.PhoneNumber,
                Email = visitor.Email,
                MomName = visitor.MomName,
                Governorate = visitor.Governorate,
                Address = visitor.Address,
                Nationality = visitor.Nationality,
                Religon = visitor.Religon
            };
            return Ok(dto);
        }

        

        [HttpPost]
        public async Task<IActionResult> AddAsync(VisitorsDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (await _userManager.FindByEmailAsync(dto.Email!) is not null && await _userManager.FindByIdAsync(dto.Id) is not null)
            {
                return BadRequest(new AuthModel { Message = "Email or id is already registered!"});
            }

            if(await _userManager.Users.SingleOrDefaultAsync<Visitor>(V => V.FactoryNum == dto.FactoryNum) is not null)
            {
                return BadRequest(new AuthModel { Message = "FactoryNum is wrong" });
            }



            Visitor visitor = new()
            {
                UserName = dto.Id+ "@NotaryOffice.com",
                Id = dto.Id,
                FactoryNum = dto.FactoryNum,
                Name = dto.Name,
                PhoneNumber = dto.Phone,
                Email = dto.Email,
                MomName = dto.MomName,
                Governorate = dto.Governorate,
                Address = dto.Address,
                Nationality = dto.Nationality,
                Religon = dto.Religon
            };



            var result = await _userManager.CreateAsync(visitor, dto.Password);
            if (!result.Succeeded)
            {
                string errors = string.Empty;
                foreach(var error in result.Errors)
                {
                    errors += error.Description + ",";
                }
                return BadRequest(new AuthModel { Message = errors });
            }



            await _userManager.AddToRoleAsync(visitor,"User");
            var jwtToken = await CreateJwtToken(visitor);

            return Ok(new AuthModel
            {
                Email = visitor.Email,
                ExpiresOn = jwtToken.ValidTo,
                IsAuthenticated = true,
                Roles = new List<string> { "User" },
                Token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                Username = visitor.UserName
            });
        }





        [HttpPost("SignIn")]

        public async Task<IActionResult> SignIn(SignInDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var authModel = new AuthModel();

            var visitor = await _userManager.FindByEmailAsync(dto.Email!);
            if(visitor is null || !await _userManager.CheckPasswordAsync(visitor, dto.Password))
            {
                authModel.Message = "Email or password is incorrecrt";
                return BadRequest(authModel);
            }
            
            var jwtToken = await CreateJwtToken(visitor);

            authModel.Email = visitor.Email;
            authModel.ExpiresOn = jwtToken.ValidTo;
            authModel.IsAuthenticated = true;
            authModel.Roles = (await _userManager.GetRolesAsync(visitor)).ToList();
            authModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            authModel.Username = visitor.UserName;

            return Ok(authModel);

        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(VisitorsDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var visitor = await _userManager.FindByIdAsync(dto.Id);

            if (visitor is null)
            {
                return BadRequest(new AuthModel { Message = "There are not user with this id" });
            }

            visitor.Name = dto.Name;
            visitor.PhoneNumber = dto.Phone;
            visitor.Email = dto.Email;
            visitor.MomName = dto.MomName;
            visitor.Governorate = dto.Governorate;
            visitor.Address = dto.Address;
            visitor.Nationality = dto.Nationality;
            visitor.Religon = dto.Religon;

            var result = await _userManager.UpdateAsync(visitor);
            if (!result.Succeeded)
            {
                string errors = string.Empty;
                foreach (var error in result.Errors)
                {
                    errors += error.Description + ",";
                }
                return BadRequest(new AuthModel { Message = errors });
            }
            return Ok(visitor);
        }



        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            var visitor = await _userManager.FindByIdAsync(id);
            if (visitor is null)
                return NotFound($"There are not any usre with id = {id}");
            await _userManager.DeleteAsync(visitor);
            return Ok("This account has been deleted");
        }

        [HttpPost("AddRole")]
        public async Task<IActionResult> AddRoleAsync(AddRoleModle model)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.FindByIdAsync(model.UserId);

            if (user is null || !await _roleManager.RoleExistsAsync(model.RloeName))
            {
                return BadRequest("Invalid User ID or Rloe Name");
            }

            if (await _userManager.IsInRoleAsync(user, model.RloeName))
            {
                return BadRequest("User is alredy assignd to this role");
            }

            var result = await _userManager.AddToRoleAsync(user, model.RloeName);

            if (result.Succeeded)
            {
                return Ok(model);
            }

            return BadRequest("Somesing wrong");
        }




        private async Task<JwtSecurityToken> CreateJwtToken(Visitor visitor)
        {
            var userClaims = await _userManager.GetClaimsAsync(visitor);
            var roles = await _userManager.GetRolesAsync(visitor);
            var roleClaims = new List<Claim>();

            foreach (var role in roles)
                roleClaims.Add(new Claim("roles", role));

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, visitor.UserName!),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, visitor.Email!),
                new Claim("uid", visitor.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key!));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
            audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(_jwt.DurationInDays),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }

    }
}
