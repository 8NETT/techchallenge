using FIAP.FCG.Core.Entity;

namespace FIAP.FCG.Core.Repository;

public interface IUsuarioRepository : IRepository<Usuario>  
{
    Task<Usuario?> ObterPorEmailAsync(string email);
    Task<Usuario?> ObterComBibliotecaAsync(int id);
    Task<Usuario?> ObterComComprasAsync(int id);
    Task<IEnumerable<Jogo>> ObterJogosPorUsuarioAsync(int usuarioId);
}