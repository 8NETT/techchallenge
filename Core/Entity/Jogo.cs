namespace Core.Entity;

public class Jogo : EntityBase
{
    public required string Nome { get; set; }
    public string? Descricao { get; set; }
    public required decimal Valor { get; set; }
    public int Desconto { get; set; }
    public ICollection<Usuario> Usuarios { get; set; }
}