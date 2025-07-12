using FIAP.FCG.Core.Entity;

namespace FIAP.FCG.Core.Repository
{
    public interface ICompraRepository : IRepository<Compra>
    {
        Task<IEnumerable<Compra>> ObterDoComprador(int usuarioId);
        Task<Compra?> ObterComCompradorBiblioteca(int id);
    }
}
