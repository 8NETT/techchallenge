using Core.Entity;
using Core.Repository;
using Microsoft.EntityFrameworkCore;

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

    public void VincularJogoAoUsuario(int jogoId, int usuarioId)
    {
        var jogo = _context.Jogo.Include(j => j.Usuarios).FirstOrDefault(j => j.Id == jogoId) ?? throw new Exception("Jogo não encontrado.");

        var usuario = _context.Usuario.Find(usuarioId) ?? throw new Exception("Usuário não encontrado.");

        if (jogo.Usuarios.All(u => u.Id != usuarioId))
        {
            jogo.Usuarios.Add(usuario);
            _context.SaveChanges();
        }
    }

    public void DesvincularJogoDoUsuario(int jogoId, int usuarioId)
    {
        var jogo = _context.Jogo.Include(j => j.Usuarios).FirstOrDefault(j => j.Id == jogoId) ?? throw new Exception("Jogo não encontrado.");

        var usuario = jogo.Usuarios.FirstOrDefault(u => u.Id == usuarioId) ?? throw new Exception("Usuário não vinculado a este jogo.");

        jogo.Usuarios.Remove(usuario);
        _context.SaveChanges();
    }
}