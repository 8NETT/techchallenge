namespace FIAP.FCG.Application.DTOs
{
    public class CompraDTO
    {
        public required int Id { get; set; }
        public required int UsuarioId { get; set; }
        public required int JogoId { get; set; }
        public required decimal Valor { get; set; }
        public required int Desconto { get; set; }
        public required decimal Total { get; set; }
    }
}
