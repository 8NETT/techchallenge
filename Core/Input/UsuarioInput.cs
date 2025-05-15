using System.ComponentModel.DataAnnotations;

namespace Core.Input;

public class UsuarioInput
{
    public required string Nome { get; set; }
    [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[^A-Za-z0-9]).{8,}$", 
        ErrorMessage = "A senha precisa conter caractere especial, letra e número.")]
    [StringLength(60, MinimumLength = 8, ErrorMessage = "A senha deve ter entre 8 e 60 caracteres.")]
    public required string Password { get; set; }
    [EmailAddress(ErrorMessage= "Digite um email válido")]
    [DataType(DataType.EmailAddress)]
    public required string Email { get; set; }  
}