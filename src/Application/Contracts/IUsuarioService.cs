using Ardalis.Result;
using FIAP.FCG.Application.DTOs;

namespace FIAP.FCG.Application.Contracts
{
    public interface IUsuarioService : IDisposable
    {
        Task<IEnumerable<UsuarioDTO>> ObterTodosAsync();
        Task<Result<UsuarioDTO>> ObterPorIdAsync(int id);
        Task<Result<UsuarioDTO>> CadastrarAsync(CadastrarUsuarioDTO dto);
        Task<Result<UsuarioDTO>> AlterarAsync(AlterarUsuarioDTO dto);
        Task<Result> DeletarAsync(int id);
        Task<Result<UsuarioDTO>> LoginAsync(LoginDTO dto);
    }
}
