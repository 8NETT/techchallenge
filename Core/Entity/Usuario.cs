namespace FIAP.FCG.Core.Entity;

public class Usuario : EntityBase
{
    public required string Nome { get; set; }
    public required string Password { get; set; }
    public required string Email { get; set; }
    public required bool Profile { get; set; }
    public ICollection<Jogo> Biblioteca { get; set; }
}