using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ROUTEAPI.Dtos;

namespace ROUTEAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BlockController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public BlockController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("~/api/Block/GetAllCities")]
        public async Task<Response<List<Block>>> GetAllBlocks()
        {
            var responce = new Response<List<Block>>();
            var records = await _context.Blocks
                .Include(r => r.District)
                .ToListAsync();
            if (records != null)
            {
                responce.Message = "Success";
                responce.MessageCode = 200;
                responce.Data = records;
                responce.Success = true;
            }
            else
            {
                responce.Message = "NO Blocks in the database";
                responce.MessageCode = 200;
                responce.Data = null;
                responce.Success = true;
            }
            return responce;
        }
        [HttpPost]
        [Route("~/api/Block/Create")]
        public async Task<Response<Block>> Create(BlockDto dto)
        {
            var responce = new Response<Block>();
            if ( dto != null )
            {
                var Block = new Block { Name = dto.Name, districtId = dto.DistrictId };
                _context.Blocks.Add(Block);
                await _context.SaveChangesAsync();
                responce.Message = "Success";
                responce.MessageCode = 200;
                responce.Data = Block;
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
        [Route("~/api/Block/Update")]
        public async Task<Response<Block>> Update(int id , BlockDto dto)
        {
            var responce = new Response<Block>();
            var Block = _context.Blocks.FirstOrDefault(b => b.Id == id);
            if ( Block != null)
            {
                Block.Name = dto.Name;Block.districtId = dto.DistrictId;
                _context.Blocks.Update(Block);
                await _context.SaveChangesAsync();
                responce.Message = "Success";
                responce.MessageCode = 200;
                responce.Data = Block;
                responce.Success = true;
            }
            else
            {
                responce.Message = "No Block With This Id";
                responce.MessageCode = 400;
                responce.Data = null;
                responce.Success = false;
            }
            return responce;
        }
        [HttpDelete]
        [Route("~/api/Block/Delete")]
        public async Task<Response<Block>> Delete(int id)
        {
            var responce = new Response<Block>();
            var Block = _context.Blocks.FirstOrDefault(b => b.Id == id);
            if ( Block != null)
            {
                _context.Blocks.Remove(Block);
                await _context.SaveChangesAsync();
                responce.Message = "Success";
                responce.MessageCode = 200;
                responce.Data = Block;
                responce.Success = true;
            }
            else
            {
                responce.Message = "No Block With This Id";
                responce.MessageCode = 400;
                responce.Data = null;
                responce.Success = false;
            }
            return responce;
        }
        [HttpGet]
        [Route("~/api/Block/GetById")]
        public async Task<Response<Block>> GetById(int id)
        {
            var responce = new Response<Block>();
            var Block = _context.Blocks.FirstOrDefault(b => b.Id == id);
            
            if (Block != null)
            {
                responce.Message = "Success";
                responce.MessageCode = 200;
                responce.Data = Block;
                responce.Success = true;
            }
            else
            {
                responce.Message = "No Block With This Id";
                responce.MessageCode = 400;
                responce.Data = null;
                responce.Success = false;
            }
            return responce;
        }
    }
}
