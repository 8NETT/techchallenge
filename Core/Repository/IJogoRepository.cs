using Core.Entity;

namespace Core.Repository;

public interface IJogoRepository : IRepository<Jogo>
{
    Task<Jogo?> ObterPorNomeAsync(string nome);
}