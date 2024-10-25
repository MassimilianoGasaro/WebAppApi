using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebAppApi.Data;
using WebAppApi.DTOs;
using WebAppApi.Entities;

namespace WebAppApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExpensesController(DataContext dataContext) : ControllerBase
    {
        private readonly DataContext _dataContext = dataContext;
        private Expense _expenseItem = new();

        [HttpGet("{id:int}")]
        public async Task<ActionResult<IEnumerable<Expense>>> GetExpenses([FromRoute] int id)
        {
            try
            {
                List<Expense> expensesList = await _dataContext.Expenses
                    .Where(exp => exp.UserId == id)
                    .AsNoTracking()
                    .ToListAsync();

                return Ok(expensesList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{id:int}")]
        public async Task<IActionResult> InsertExpense([FromRoute] int id, [FromBody] ExpenseDTO expenseRequest )
        {
            try
            {
                // Recupera l'ID dell'utente autenticato
                string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                // Converte l'ID dell'utente autenticato in un intero
                if (!int.TryParse(userId, out int authenticatedUserId))
                {
                    return Unauthorized("Invalid user ID.");
                }

                // Verifica che l'ID dell'utente autenticato corrisponda all'ID passato nella route
                if (authenticatedUserId != id)
                {
                    return Forbid("You are not authorized to perform this action.");
                }

                _expenseItem = new()
                {
                    UserId = id,
                };

                _dataContext.Expenses.Add(_expenseItem);
                await _dataContext.SaveChangesAsync();

                return CreatedAtAction(nameof(GetExpenses), new { id = _expenseItem.Id }, _expenseItem);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
