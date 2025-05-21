using Core.Entity;
using Core.Repository;

namespace Infrastructure.Repository;

public class UsuarioRepository : EFRepository<Usuario>, IUsuarioRepository
{
    public UsuarioRepository(ApplicationDbContext context) : base(context)
    {
    }
    public IEnumerable<Jogo> ObterJogosPorUsuario(int usuarioId)
    {
        return _context.Jogo.Where(j => j.Usuarios.Any(u => u.Id == usuarioId)).ToList();
    }
}