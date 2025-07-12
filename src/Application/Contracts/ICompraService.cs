using Ardalis.Result;
using FIAP.FCG.Application.DTOs;

namespace FIAP.FCG.Application.Contracts
{
    public interface ICompraService : IDisposable
    {
        Task<Result<IEnumerable<CompraDTO>>> ObterDoUsuarioAsync(int usuarioId);
        Task<Result> ComprarAsync(int usuarioId, int jogoId);
        Task<Result> EstornarAsync(int id);
    }
}
