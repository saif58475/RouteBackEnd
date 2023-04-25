using Managment_System.Responce;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ROUTEAPI.Dtos;

namespace ROUTEAPI.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public CityController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("~/api/City/GetAllCities")]
        public async Task<Response<List<City>>> GetAllCities()
        {
            var responce = new Response<List<City>>();
            var records = await _context.Cities
                .Include(r => r.Country)
                .ToListAsync();
          if ( records != null)
            {
                responce.Message = "Success";
                responce.MessageCode = 200;
                responce.Data = records;
                responce.Success = true;
            }
            else
            {
                responce.Message = "NO Cities in the database";
                responce.MessageCode = 200;
                responce.Data = null;
                responce.Success = true;
            }
                return responce;
        }
        [HttpPost]
        [Route("~/api/City/Create")]
        public async Task<Response<City>> Create(CityDto dto)
        {
            var responce = new Response<City>();
            if ( dto != null )
            {
                var city = new City { Name = dto.Name, CountryId = dto.CountryId };
                _context.Cities.Add(city);
                await _context.SaveChangesAsync();
                responce.Message = "Success";
                responce.MessageCode = 200;
                responce.Data = city;
                responce.Success = true;
            }
            else
            {
                responce.Message = "Failed";
                responce.MessageCode = 400;
                responce.Data = null;
                responce.Success = false;
            }
            return responce;
        }
        [HttpPut]
        [Route("~/api/City/Update")]
        public async Task<Response<City>> Update(int id , CityDto dto)
        {
            var responce = new Response<City>();
            var city = _context.Cities.FirstOrDefault(c => c.Id == id);
            if ( city != null )
            {
                city.Name = dto.Name;city.CountryId = dto.CountryId;
                _context.Cities.Update(city);
                await _context.SaveChangesAsync();
                responce.Message = "Success";
                responce.MessageCode = 200;
                responce.Data = city;
                responce.Success = true;
            }
            else
            {
                responce.Message = "Failed";
                responce.MessageCode = 400;
                responce.Data = null;
                responce.Success = false;
            }
            return responce;
        }
        [HttpDelete]
        [Route("~/api/City/Delete")]
        public async Task<Response<City>> Delete(int id)
        {
            var responce = new Response<City>();
            var city = _context.Cities.FirstOrDefault(c => c.Id == id);
            if ( city != null ) 
            {
                _context.Cities.Remove(city);
                await _context.SaveChangesAsync();
                responce.Message = "Success";
                responce.MessageCode = 200;
                responce.Data = city;
                responce.Success = true;
            }
            else
            {
                responce.Message = "Failed";
                responce.MessageCode = 400;
                responce.Data = null;
                responce.Success = false;
            }
            return responce;
        }
        [HttpGet]
        [Route("~/api/City/GetById")]
        public async Task<Response<City>> GetById(int id)
        {
            var responce = new Response<City>();
            var city = await _context.Cities
                .Include(c => c.Country)
                .FirstOrDefaultAsync(c => c.Id == id);
            if (city != null)
            {
                responce.Message = "Success";
                responce.MessageCode = 200;
                responce.Data = city;
                responce.Success = true;
            }
            else
            {
                responce.Message = "Failed";
                responce.MessageCode = 400;
                responce.Data = null;
                responce.Success = false;
            }
            return responce;
        }
    }
}
