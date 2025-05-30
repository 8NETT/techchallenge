using FIAP.FCG.Core.Entity;
using FIAP.FCG.Core.Repository;
using Microsoft.EntityFrameworkCore;

namespace FIAP.FCG.Infrastructure.Repository;

public class EFRepository<T> : IRepository<T> where T : EntityBase   
{
    protected ApplicationDbContext _context;
    protected DbSet<T> _dbSet;

    public EFRepository(ApplicationDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public async Task<IEnumerable<T>> ObterTodosAsync() =>
        await _dbSet.ToArrayAsync();

    public async Task<T?> ObterPorIdAsync(int id) =>
        await _dbSet.FirstOrDefaultAsync(e => e.Id == id);

    public void Cadastrar(T entidade)
    {
        entidade.DataCriacao = DateTime.Now;
        _dbSet.Add(entidade);
    }

    public void Alterar(T entidade) =>
        _dbSet.Remove(entidade);

    public void Deletar(T entidade) =>
        _dbSet.Remove(entidade);
}