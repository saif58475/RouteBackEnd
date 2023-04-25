using Managment_System.Responce;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ROUTEAPI.Dtos;
using ROUTEAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ROUTEAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public readonly ApplicationDbContext _context;
        public IConfiguration _configration;    
        public UserController(ApplicationDbContext context, IConfiguration configuration)
        {
            _configration = configuration;
            _context = context;
        }

        [HttpGet]
        [Route("~/api/User/GetAllUsers")]
       public async Task<Response<List<User>>> GetAllUsers()
        {
            var responce = new Response<List<User>>();
            var records = await _context.users.ToListAsync();
            try
            {
                responce.Message = "Success";
                responce.MessageCode = 200;
                responce.Data = records;
                responce.Success = true;
            }
            catch (Exception ex)
            {
                
                responce.Message = ex.Message;
                responce.MessageCode = 200;
                responce.Data = null;
                responce.Success = true;
            }
            return responce;
        }
        [HttpPost]
        [Route("~/api/User/Create")]
        public async Task<Response<User>> Create(UserDto dto)
        {
            var responce = new Response<User>();
            try
            {
                var record = new User { Name = dto.Name, Password = dto.Password };
                _context.users.Add(record);
                await _context.SaveChangesAsync();
                responce.Message = "Success";
                responce.MessageCode = 200;
                responce.Data = record;
                responce.Success = true;
            }
            catch (Exception ex)
            {
                responce.Message = ex.Message;
                responce.MessageCode = 400;
                responce.Data = null;
                responce.Success = false;
            }
            return responce;
        }
        [HttpPut]
        [Route("~/api/User/Update")]
        public async Task<Response<User>> Update(int id , UserDto dto)
        {
            var responce = new Response<User>();
            try
            {
                var record = _context.users.SingleOrDefault(r => r.Id == id);
                record.Name = dto.Name; record.Password = dto.Password;
                _context.users.Update(record);
                await _context.SaveChangesAsync();
                responce.Message = "Success";
                responce.MessageCode = 200;
                responce.Data = record;
                responce.Success = true;
            }
            catch (Exception ex)
            {
                responce.Message = ex.Message;
                responce.MessageCode = 200;
                responce.Data = null;
                responce.Success = true;
            }
            return responce;
        }
        [HttpDelete]
        [Route("~/api/User/Delete")]
        public async Task<Response<User>> Delete(int id)
        {
            var responce = new Response<User>();
            try
            {
                var record = _context.users.SingleOrDefault(r => r.Id == id);
                _context.users.Remove(record);
                await _context.SaveChangesAsync();
                responce.Message = "Success";
                responce.MessageCode = 200;
                responce.Data = record;
                responce.Success = true;
            }
            catch (Exception ex)
            {
                responce.Message = "No User With This Id";
                responce.MessageCode = 200;
                responce.Data = null;
                responce.Success = true;
            }
            return responce;
        }
      
        [AllowAnonymous]
        [HttpPost]
        [Route("~/api/User/Login")]
        public string Login(UserDto dto)
        {
            var responce = new Response<User>();
            var record = _context.users.Where(u => u.Name == dto.Name && u.Password == dto.Password).FirstOrDefault();
            if (record == null)
            {
                return responce.Message = "Failed To Login";
            }
            else
            {
                return GENERATEJSONWEBTOKEN(record.Name, record.Id);
            }
        }
        [NonAction]
        public string GENERATEJSONWEBTOKEN(string username, int id)
        {

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName,username),
                new Claim(JwtRegisteredClaimNames.Sub,id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var token = new JwtSecurityToken(
                issuer: _configration["Jwt:Issuer"],
                audience: _configration["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddDays(2),
                signingCredentials: credentials);
            var encodeToken = new JwtSecurityTokenHandler().WriteToken(token);
            return encodeToken;


        }
    }
}
