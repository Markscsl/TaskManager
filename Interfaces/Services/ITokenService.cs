using TaskManager.Models.Entities;

namespace TaskManager.Interfaces.Services
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}
