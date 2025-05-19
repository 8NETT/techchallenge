using System.ComponentModel.DataAnnotations;

namespace Core.Input;

public class JogoInput
{
    public required string Nome { get; set; }
    public required int Valor { get; set; }
    [Range(0, 100, ErrorMessage = "Valor deve ser entre 0 e 100.") ]
    public required int Desconto { get; set; }
}