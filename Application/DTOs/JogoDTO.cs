namespace Application.DTOs
{
    public class JogoDTO
    {
        public required string Nome { get; set; }
        public required decimal Valor { get; set; }
        public required int Desconto { get; set; }
    }
}
