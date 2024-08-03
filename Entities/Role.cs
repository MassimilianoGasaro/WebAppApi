using Microsoft.AspNetCore.Identity;

namespace WebAppApi.Entities
{
    public class Role : IdentityRole<int>
    {
        public ICollection<UserRole> UserRoles { get; set; }
    }
}
