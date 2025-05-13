using Core.Entity;
using Core.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

public class EFRepository<T> : IRepository<T> where T : EntityBase   
{
    protected ApplicationDbContext _context;
    protected DbSet<T> _dbSet;

    public EFRepository(ApplicationDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public IList<T> ObterTodos()
    {
        return _dbSet.ToList();
    }

    public T ObterPorId(int id)
    {
        return _dbSet.FirstOrDefault(entiry => entiry.Id == id);
    }

    public void Cadastar(T entidade)
    {
        entidade.DataCriacao = DateTime.Now;
        _dbSet.Add(entidade);
        _context.SaveChanges(); 
    }

    public void Alterar(T entidade)
    {
        _dbSet.Update(entidade);
        _context.SaveChanges();
    }

    public void Deletar(int id)
    {
        _dbSet.Remove(ObterPorId(id));
        _context.SaveChanges();
    }
}