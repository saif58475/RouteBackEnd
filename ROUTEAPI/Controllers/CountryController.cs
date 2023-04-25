using Managment_System.Responce;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ROUTEAPI.Dtos;

namespace ROUTEAPI.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public CountryController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        [Route("~/api/Country/GetAllCountries")]
        public async Task<Response<List<Country>>> GetAllCountries()
        {
            var responce = new Response<List<Country>>();
            var records = await _context.Countries
                .Include(r => r.Cities)
                .ToListAsync();
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
        [Route("~/api/Country/Create")]
        public async Task<Response<Country>> Create([FromForm] CountryDto dto)
        {
            var responce = new Response<Country>();
            if ( dto != null)
            {
                FileInfo imgFileInfo = new FileInfo(dto.Image.FileName);
                string imgpath = Guid.NewGuid().ToString() + imgFileInfo.Extension;
                string path = Path.Combine("Images/Flags", imgpath);
                using (Stream   stream = new FileStream(path, FileMode.Create))
                {
                    dto.Image.CopyTo(stream);
                }
                var record = new Country { Name = dto.Name, PhoneCode = dto.PhoneCode, Image = path };
                _context.Countries.Add(record);
                await _context.SaveChangesAsync();
                responce.Message = "Succcess";
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
        [Route("~/api/Country/Update")]
        public async Task<Response<Country>> Update(int id, [FromForm] CountryDto dto)
        {
            var responce = new Response<Country>();
            var record = _context.Countries.FirstOrDefault(r => r.Id == id);
            if (record != null)
            {
                if ( dto.Image == null)
                {
                    record.Name = dto.Name; record.PhoneCode = dto.PhoneCode;
                }
                else
                {
                    using (Stream stream = new FileStream(record.Image, FileMode.Create))
                    {
                        dto.Image.CopyTo(stream);
                    };
                    record.Name = dto.Name; record.PhoneCode = dto.PhoneCode;
                }
                _context.Countries.Update(record);
                await _context.SaveChangesAsync();
                responce.Message = "Succcess";
                responce.MessageCode = 200;
                responce.Data = record;
                responce.Success = true;
            }
            else
            {
                responce.Message = "No Country with this id";
                responce.MessageCode = 400;
                responce.Data = null;
                responce.Success = false;
            }
            return responce;
        }
        [HttpDelete]
        [Route("~/api/Country/Delete")]
        public async Task<Response<Country>> Delete(int id)
        {
            var responce = new Response<Country>();
            var record = _context.Countries.FirstOrDefault(r => r.Id == id);
            if ( record != null)
            {
                _context.Countries.Remove(record);
                await _context.SaveChangesAsync();
                responce.Message = "Succcess";
                responce.MessageCode = 200;
                responce.Data = record;
                responce.Success = true;
            }
            else
            {
                responce.Message = "Their is no Country With this id";
                responce.MessageCode = 200;
                responce.Data = null;
                responce.Success = true;
            }
            return responce;
        }
        [HttpGet]
        [Route("~/api/Country/GetById")]
        public async Task<Response<ReturnCountryDto>> GetById(int id)
        {
            var responce = new Response<ReturnCountryDto>();
            var record = _context.Countries
                .Include(r => r.Cities)
                .FirstOrDefault(r => r.Id == id);
            ReturnCountryDto Country = new ReturnCountryDto { Id = record.Id, Name = record.Name, Image = record.Image, PhoneCode = record.PhoneCode, Cities = record.Cities };
            if (record != null)
            {
                responce.Message = "Succcess";
                responce.MessageCode = 200;
                responce.Data = Country;
                responce.Success = true;
            }
            else
            {
                responce.Message = "Their is no Country With this id";
                responce.MessageCode = 200;
                responce.Data = null;
                responce.Success = true;
            }
            return responce;
        }

    }
   
}
