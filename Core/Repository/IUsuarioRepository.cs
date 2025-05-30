using Core.Entity;

namespace Core.Repository;

public interface IUsuarioRepository : IRepository<Usuario>  
{
    Task<Usuario?> ObterPorEmailAsync(string email);
    Task<Usuario?> ObterComBibliotecaAsync(int id);
    Task<IEnumerable<Jogo>> ObterJogosPorUsuarioAsync(int usuarioId);
    Task VincularJogoAoUsuarioAsync(int jogoId, int usuarioId);
    Task DesvincularJogoDoUsuarioAsync(int jogoId, int usuarioId);
}