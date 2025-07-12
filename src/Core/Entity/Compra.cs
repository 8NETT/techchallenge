using FIAP.FCG.Core.Builders;

namespace FIAP.FCG.Core.Entity
{
    public class Compra : EntityBase
    {
        public int CompradorId { get; protected internal set; }
        public int JogoId { get; protected internal set; }
        public decimal Valor { get; protected internal set; }
        public int Desconto { get; protected internal set; }
        public decimal Total { get; protected internal set; }
        public Usuario Comprador { get; protected internal set; }
        public Jogo Jogo { get; protected internal set; }

        protected internal Compra() { }

        public static CompraBuilder New() => new CompraBuilder();
    }
}
