using Core.Entity;

namespace Core.Repository;

public interface IUsuarioRepository : IRepository<Usuario>  
{
    IEnumerable<Jogo> ObterJogosPorUsuario(int usuarioId);
}