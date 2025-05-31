using Ardalis.Result;
using FIAP.FCG.Application.Contracts;
using FIAP.FCG.Core.Repository;

namespace FIAP.FCG.Application.Services
{
    public class CompraService : ICompraService
    {
        private IUnitOfWork _unitOfWork;

        public CompraService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> ComprarAsync(int usuarioId, int jogoId)
        {
            var usuario = await _unitOfWork.UsuarioRepository.ObterComBibliotecaAsync(usuarioId);

            if (usuario == null)
                return Result.NotFound("Usuário não localizado.");

            var jogo = await _unitOfWork.JogoRepository.ObterPorIdAsync(jogoId);

            if (jogo == null)
                return Result.NotFound("Jogo não localizado.");
            if (usuario.Biblioteca.Contains(jogo))
                return Result.Conflict("Usuário já possui o jogo em sua biblioteca.");

            usuario.Biblioteca.Add(jogo);
            _unitOfWork.UsuarioRepository.Alterar(usuario);
            await _unitOfWork.CommitAsync();

            return Result.Success();
        }

        public async Task<Result> EstornarAsync(int usuarioId, int jogoId)
        {
            var usuario = await _unitOfWork.UsuarioRepository.ObterComBibliotecaAsync(usuarioId);

            if (usuario == null)
                return Result.NotFound("Usuário não localizado.");

            var jogo = await _unitOfWork.JogoRepository.ObterPorIdAsync(jogoId);

            if (jogo == null)
                return Result.NotFound("Jogo não localizado.");
            if (!usuario.Biblioteca.Contains(jogo))
                return Result.Conflict("Usuário não possui o jogo em sua biblioteca.");

            usuario.Biblioteca.Remove(jogo);
            _unitOfWork.UsuarioRepository.Alterar(usuario);
            await _unitOfWork.CommitAsync();

            return Result.Success();
        }

        public void Dispose() => _unitOfWork.Dispose();
    }
}
