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

    public async Task<Usuario?> ObterPorEmailAsync(string email) =>
        await _dbSet.SingleOrDefaultAsync(u => u.Email == email);

    public async Task<Usuario?> ObterComBibliotecaAsync(int id) =>
        await _dbSet.Include(u => u.Biblioteca).SingleOrDefaultAsync(u => u.Id == id);

    public async Task<Usuario?> ObterComComprasAsync(int id) =>
        await _dbSet.Include(u => u.Compras).ThenInclude(u => u.Jogo).SingleOrDefaultAsync(u => u.Id == id);
}