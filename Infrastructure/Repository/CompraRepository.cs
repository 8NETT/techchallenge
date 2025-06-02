using FIAP.FCG.Core.Entity;
using FIAP.FCG.Core.Repository;
using Microsoft.EntityFrameworkCore;

namespace FIAP.FCG.Infrastructure.Repository
{
    public class CompraRepository : EFRepository<Compra>, ICompraRepository
    {
        public CompraRepository(ApplicationDbContext context) : base(context) { }

        public async Task<Compra?> ObterComCompradorBiblioteca(int id) =>
            await _dbSet
                .Include(c => c.Comprador)
                .ThenInclude(u => u.Biblioteca)
                .SingleOrDefaultAsync(c => c.Id == id);

        public async Task<IEnumerable<Compra>> ObterDoComprador(int usuarioId) =>
            await _dbSet.Where(c => c.Comprador.Id == usuarioId).ToArrayAsync();
    }
}
