using TaskManager.Models.Entities;

namespace TaskManager.Interfaces.Services
{
    public interface IUserService
    {
        Task<User> RegisterAsync(User user, string password);
        Task<User?> ValidateCredentialsAsync(string email, string password);
        Task<bool> ChangePasswordAsync(int userLoggedInId, string oldPassword, string newPassword);
        Task<User?> GetUserProfileAsync(int userId);
        Task<User> UpdateUserProfileAsync(int userId, User user);
    }
}