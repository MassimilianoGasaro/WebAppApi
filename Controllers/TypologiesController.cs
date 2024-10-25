using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppApi.Data;
using WebAppApi.Entities;

namespace WebAppApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TypologiesController(DataContext dataContext) : ControllerBase
    {
        private readonly DataContext _dataContext = dataContext;

        [HttpGet]
        public async Task<ActionResult> GetTypes()
        {
            try
            {
                List<Typology> types = await _dataContext.Typologies.ToListAsync();
                return Ok(types);
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
