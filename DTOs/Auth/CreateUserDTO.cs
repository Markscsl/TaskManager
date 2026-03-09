using System.ComponentModel.DataAnnotations;

namespace TaskManager.DTOs.Auth
{
    public class CreateUserDTO
    {
        [Required(ErrorMessage = "O nome é obrigatório.")]
        [MinLength(3, ErrorMessage = "O nome deve ter no mínimo 3 caracteres.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "O email é obrigatório.")]
        [EmailAddress(ErrorMessage = "O formato do e-mail é inválido. (Ex: nome@dominio.com).")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "A senha é obrigatório.")]
        [MinLength(6, ErrorMessage = "A senha deve ter no mínimo 6 caracteres.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{6,}$", ErrorMessage = "A senha deve conter letras maiúsculas, minúsculas, números e caracteres especiais.")]
        public string Password { get; set; } = string.Empty;
    }
}
