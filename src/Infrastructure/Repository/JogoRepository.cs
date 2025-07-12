using FIAP.FCG.Core.Entity;
using FIAP.FCG.Core.Repository;
using Microsoft.EntityFrameworkCore;

namespace FIAP.FCG.Infrastructure.Repository;

public class JogoRepository : EFRepository<Jogo>, IJogoRepository
{
    public JogoRepository(ApplicationDbContext context) : base(context) { }

    public async Task<Jogo?> ObterPorNomeAsync(string nome) =>
        await _dbSet.SingleOrDefaultAsync(j => j.Nome == nome);
}