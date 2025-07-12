using FIAP.FCG.Core.Entity;

namespace FIAP.FCG.Core.Repository;

public interface IJogoRepository : IRepository<Jogo>
{
    Task<Jogo?> ObterPorNomeAsync(string nome);
}