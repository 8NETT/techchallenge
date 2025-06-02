using Ardalis.Result;
using FIAP.FCG.Application.Contracts;
using FIAP.FCG.Application.DTOs;
using FIAP.FCG.Application.Mappers;
using FIAP.FCG.Core.Entity;
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

        public async Task<Result<IEnumerable<CompraDTO>>> ObterDoUsuarioAsync(int usuarioId)
        {
            var usuario = await _unitOfWork.UsuarioRepository.ObterComComprasAsync(usuarioId);

            if (usuario == null)
                return Result.NotFound("Usuário não localizado.");

            return Result.Success(usuario.Compras.Select(c => c.ToDTO()));
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

            var compra = new Compra
            {
                CompradorId = usuario.Id,
                JogoId = jogo.Id,
                Valor = jogo.Valor,
                Desconto = jogo.Desconto,
                Total = jogo.Valor * (1 - (Convert.ToDecimal(jogo.Desconto) / 100M))
            };

            usuario.Biblioteca.Add(jogo);
            _unitOfWork.CompraRepository.Cadastrar(compra);
            _unitOfWork.UsuarioRepository.Alterar(usuario);
            await _unitOfWork.CommitAsync();

            return Result.Success();
        }

        public async Task<Result> EstornarAsync(int id)
        {
            var compra = await _unitOfWork.CompraRepository.ObterComCompradorBiblioteca(id);

            if (compra == null)
                return Result.NotFound("Compra não localizada.");

            compra.Comprador.Biblioteca.Remove(compra.Jogo);
            _unitOfWork.UsuarioRepository.Alterar(compra.Comprador);
            _unitOfWork.CompraRepository.Deletar(compra);
            await _unitOfWork.CommitAsync();

            return Result.Success();
        }

        public void Dispose() => _unitOfWork.Dispose();
    }
}
