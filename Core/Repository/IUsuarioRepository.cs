using Core.Entity;

namespace Core.Repository;

public interface IUsuarioRepository : IRepository<Usuario>  
{
    IEnumerable<Jogo> ObterJogosPorUsuario(int usuarioId);
    void VincularJogoAoUsuario(int jogoId, int usuarioId);
    void DesvincularJogoDoUsuario(int jogoId, int usuarioId);
}