using FIAP.FCG.Core.Builders;

namespace FIAP.FCG.Core.Entity;

public class Jogo : EntityBase
{
    public string Nome { get; protected internal set; }
    public string? Descricao { get; protected internal set; }
    public decimal Valor { get; protected internal set; }
    public int Desconto { get; protected internal set; }
    public ICollection<Usuario> Usuarios { get; set; }

    protected internal Jogo() { }

    public static JogoBuilder New() => new JogoBuilder();
}