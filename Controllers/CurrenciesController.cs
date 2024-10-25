using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppApi.Data;
using WebAppApi.Entities;

namespace WebAppApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CurrenciesController(DataContext dataContext) : Controller
    {
        private readonly DataContext _dataContext = dataContext;

        [HttpGet]
        public async Task<ActionResult> GetTypes()
        {
            try
            {
                List<Currency> currencies = await _dataContext.Currencies.ToListAsync();
                return Ok(currencies);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
