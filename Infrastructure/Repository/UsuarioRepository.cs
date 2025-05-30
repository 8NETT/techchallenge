using FIAP.FCG.Core.Entity;
using FIAP.FCG.Core.Repository;
using Microsoft.EntityFrameworkCore;

namespace FIAP.FCG.Infrastructure.Repository;

public class UsuarioRepository : EFRepository<Usuario>, IUsuarioRepository
{
    public UsuarioRepository(ApplicationDbContext context) : base(context) { }

    public async Task<IEnumerable<Jogo>> ObterJogosPorUsuarioAsync(int usuarioId)
    {
        return await _context.Jogo.Where(j => j.Usuarios.Any(u => u.Id == usuarioId)).ToArrayAsync();
    }

    public async Task VincularJogoAoUsuarioAsync(int jogoId, int usuarioId)
    {
        var jogo = await _context.Jogo.Include(j => j.Usuarios).FirstOrDefaultAsync(j => j.Id == jogoId) ?? throw new Exception("Jogo não encontrado.");
        var usuario = await _context.Usuario.FindAsync(usuarioId) ?? throw new Exception("Usuário não encontrado.");

        if (jogo.Usuarios.All(u => u.Id != usuarioId))
            jogo.Usuarios.Add(usuario);
    }

    public async Task DesvincularJogoDoUsuarioAsync(int jogoId, int usuarioId)
    {
        var jogo = await _context.Jogo.Include(j => j.Usuarios).FirstOrDefaultAsync(j => j.Id == jogoId) ?? throw new Exception("Jogo não encontrado.");
        var usuario = jogo.Usuarios.FirstOrDefault(u => u.Id == usuarioId) ?? throw new Exception("Usuário não vinculado a este jogo.");

        jogo.Usuarios.Remove(usuario);
    }

    public async Task<Usuario?> ObterPorEmailAsync(string email) =>
        await _dbSet.SingleOrDefaultAsync(u => u.Email == email);

    public async Task<Usuario?> ObterComBibliotecaAsync(int id) =>
        await _dbSet.Include(u => u.Biblioteca).SingleOrDefaultAsync(u => u.Id == id);
}