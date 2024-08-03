using WebAppApi.Entities;

namespace WebAppApi.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateToken(User user);
    }
}
