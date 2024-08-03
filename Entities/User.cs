using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting;

namespace WebAppApi.Entities
{
    public class User : IdentityUser<int>
    {
        public ICollection<UserRole> UserRoles { get; set; }
        public List<Expense> Expenses { get; set; } = [];
    }
}
