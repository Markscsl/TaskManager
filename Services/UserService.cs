using TaskManager.Interfaces.Repositories;
using TaskManager.Interfaces.Services;
using TaskManager.Models.Entities;

namespace TaskManager.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> RegisterAsync(User user, string password)
        {
            var emailExistente = await _userRepository.GetByEmailAsync(user.Email);

            if (emailExistente != null)
            {
                throw new Exception("Este e-mail já está em uso.");
            }

            string senhaHash = BCrypt.Net.BCrypt.HashPassword(password);
            user.PasswordHash = senhaHash;

            await _userRepository.AddAsync(user);

            return user;
        }

        public async Task<User?> ValidateCredentialsAsync(string email, string password)
        {
            var usuarioEncontrado = await _userRepository.GetByEmailAsync(email);

            if (usuarioEncontrado == null)
            {
                return null;
            }

            var verificacao = BCrypt.Net.BCrypt.Verify(password, usuarioEncontrado.PasswordHash);

            if (!verificacao)
            {
                return null;
            }

            return usuarioEncontrado;
        }

        public async Task<bool> ChangePasswordAsync(int userLoggedInId, string oldPassword, string newPassword)
        {
            var usuarioLogado = await _userRepository.GetByIdAsync(userLoggedInId);

            if(usuarioLogado == null)
            {
                return false;
            }

            var verificacao = BCrypt.Net.BCrypt.Verify(oldPassword, usuarioLogado.PasswordHash);

            if (!verificacao)
            {
                return false;
            }

            var novaSenhaHash = BCrypt.Net.BCrypt.HashPassword(newPassword);
            usuarioLogado.PasswordHash = novaSenhaHash;

            await _userRepository.UpdateAsync(usuarioLogado);

            return true;
        }

        public async Task<User?> GetUserProfileAsync(int userId)
        {
            return await _userRepository.GetByIdAsync(userId);
        }

        public async Task<User> UpdateUserProfileAsync(int userId, User user)
        {
            var usuarioBuscado = await _userRepository.GetByIdAsync(userId);

            if (usuarioBuscado == null)
            {
                return null;
            }

            usuarioBuscado.Name = user.Name;
            usuarioBuscado.Email = user.Email;

            await _userRepository.UpdateAsync(usuarioBuscado);
            return usuarioBuscado;
        }
    }
}
