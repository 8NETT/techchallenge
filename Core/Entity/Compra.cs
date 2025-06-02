namespace FIAP.FCG.Core.Entity
{
    public class Compra : EntityBase
    {
        public required int CompradorId { get; set; }
        public required int JogoId { get; set; }
        public required decimal Valor { get; set; }
        public required int Desconto { get; set; }
        public required decimal Total { get; set; }
        public Usuario Comprador { get; set; }
        public Jogo Jogo { get; set; }
    }
}
