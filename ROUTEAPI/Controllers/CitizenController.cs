using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ROUTEAPI.Dtos;

namespace ROUTEAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CitizenController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public CitizenController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("~/api/Citizen/GetAllCitizens")]
        public async Task<Response<List<Citizen>>> GetAllCitizens()
        {
            var responce = new Response<List<Citizen>>();
            var records = await _context.Citizens
                .Include(r => r.City)
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
                responce.Message = "Their is no records in the database";
                responce.MessageCode = 200;
                responce.Data = null;
                responce.Success = true;
            }
            return responce;
        }
        [HttpPost]
        [Route("~/api/Citizen/Create")]
        public async Task<Response<Citizen>> Create([FromForm] CitizenDto dto)
        {
            var responce = new Response<Citizen>();
            if ( dto != null)
            {
                FileInfo imgFileInfo = new FileInfo(dto.Image.FileName);
                string imgpath = Guid.NewGuid().ToString() + imgFileInfo.Extension;
                string path = Path.Combine("Images/Citizens", imgpath);
                using (Stream stream = new FileStream(path, FileMode.Create))
                {
                    dto.Image.CopyTo(stream);
                }
                Citizen record = new Citizen { Name = dto.Name, Age = dto.DOB, CityId = dto.CityId, Image = path };
                _context.Citizens.Add(record);
                await _context.SaveChangesAsync();
                responce.Message = "Success";
                responce.MessageCode = 200;
                responce.Data = record;
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
        [Route("~/api/Citizen/Update")]
        public async Task<Response<Citizen>> Update(int id, [FromForm] CitizenDto dto)
        {
            var responce = new Response<Citizen>();
            var record = _context.Citizens.FirstOrDefault(r => r.Id == id);
            if ( record != null )
            {
                if (dto.Image != null)
                {
                    using (Stream stream = new FileStream(record.Image, FileMode.Create))
                    {
                        dto.Image.CopyTo(stream);
                    }
                }
                record.Name = dto.Name; record.Age = dto.DOB; record.CityId = dto.CityId;
                _context.Citizens.Update(record);
                await _context.SaveChangesAsync();
                responce.Message = "Success";
                responce.MessageCode = 200;
                responce.Data = record;
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
        [Route("~/api/Citizen/Delete")]
        public async Task<Response<Citizen>> Delete(int id)
        {
            var responce = new Response<Citizen>();
            var record = _context.Citizens.FirstOrDefault(r => r.Id == id);
            if ( record != null)
            {
                _context.Citizens.Remove(record);
                await _context.SaveChangesAsync();
                responce.Message = "Success";
                responce.MessageCode = 200;
                responce.Data = record;
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
        [Route("~/api/Citizen/GetById")]
        public async Task<Response<Citizen>> GetById(int id)
        {
            var responce = new Response<Citizen>();
            var record = _context.Citizens
                .Include(r => r.City)
                .Include(c => c.City.Country)
                .FirstOrDefault(r => r.Id == id);
            if (record != null)
            {
                responce.Message = "Success";
                responce.MessageCode = 200;
                responce.Data = record;
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
