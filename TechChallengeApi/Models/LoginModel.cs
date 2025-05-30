using System.ComponentModel.DataAnnotations;

namespace FIAP.FCG.WebApi.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "O campo {0} precisa ser preenchido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo {0} precisa ser preenchido.")]
        public string Password { get; set; }
    }
}
