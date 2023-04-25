global using Managment_System.Responce;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ROUTEAPI.Dtos;


namespace ROUTEAPI.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DistrictController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public DistrictController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("~/api/District/GetAllDistricts")]
        public async Task<Response<List<District>>> GetAllDistricts()
        {
            var responce = new Response<List<District>>();
            var records = await _context.Districts.ToListAsync();
            if (records != null)
            {
                responce.Message = "Success";
                responce.MessageCode = 200;
                responce.Data = records;
                responce.Success = true;
            }
            else
            {
                responce.Message = "NO Districts in the database";
                responce.MessageCode = 200;
                responce.Data = null;
                responce.Success = true;
            }
            return responce;
        }
        [HttpPost]
        [Route("~/api/District/Create")]
        public async Task<Response<District>> Create(DistrictDto dto)
        {
            var responce = new Response<District>();
            if ( dto != null)
            {
                var District = new District { Name = dto.Name, CityId = dto.CityId };
                _context.Districts.Add(District);
                await _context.SaveChangesAsync();
                responce.Message = "Success";
                responce.MessageCode = 200;
                responce.Data = District;
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
        [Route("~/api/District/Update")]
        public async Task<Response<District>> Update(int id , DistrictDto dto)
        {
            var responce = new Response<District>();
            var district = _context.Districts.FirstOrDefault(d => d.Id == id);
            if ( district != null )
            {
                district.Name = dto.Name; district.CityId = dto.CityId;
                _context.Districts.Update(district);
                await _context.SaveChangesAsync();
                responce.Message = "Success";
                responce.MessageCode = 200;
                responce.Data = district;
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
        [Route("~/api/District/Delete")]
        public async Task<Response<District>> Delete(int id)
        {
            var responce = new Response<District>();
            var district = _context.Districts.FirstOrDefault(d => d.Id == id);
            if ( district != null )
            {
                _context.Districts.Remove(district);
                await _context.SaveChangesAsync();
                responce.Message = "Success";
                responce.MessageCode = 200;
                responce.Data = district;
                responce.Success = true;
            }
            else
            {

                responce.Message = "Their is no district with this id";
                responce.MessageCode = 400;
                responce.Data = null;
                responce.Success = false;
            }
            return responce;
        }
        [HttpGet]
        [Route("~/api/District/GetById")]
        public async Task<Response<District>> GetById(int id)
        {
            var responce = new Response<District>();
            var district = _context.Districts
                .Include(d => d.City)
                .Include(d => d.City.Country)
                .FirstOrDefault(d => d.Id == id);
            if (district != null)
            {
                responce.Message = "Success";
                responce.MessageCode = 200;
                responce.Data = district;
                responce.Success = true;
            }
            else
            {
                responce.Message = "Their is no district with this id";
                responce.MessageCode = 400;
                responce.Data = null;
                responce.Success = false;
            }
            return responce;
        }
    }
}
