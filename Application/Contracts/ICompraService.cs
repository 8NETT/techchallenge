using Ardalis.Result;

namespace FIAP.FCG.Application.Contracts
{
    public interface ICompraService : IDisposable
    {
        Task<Result> ComprarAsync(int usuarioId, int jogoId);
        Task<Result> EstornarAsync(int usuarioId, int jogoId);
    }
}
