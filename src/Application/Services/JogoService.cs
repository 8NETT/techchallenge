﻿using Ardalis.Result;
using FIAP.FCG.Application.Contracts;
using FIAP.FCG.Application.DTOs;
using FIAP.FCG.Application.Mappers;
using FIAP.FCG.Core.Repository;

namespace FIAP.FCG.Application.Services
{
    public class JogoService : BaseService, IJogoService
    {
        private IUnitOfWork _unitOfWork;
        
        public JogoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<JogoDTO>> ObterTodosAsync()
        {
            var jogos = await _unitOfWork.JogoRepository.ObterTodosAsync();
            return jogos.Select(j => j.ToDTO());
        }

        public async Task<Result<JogoDTO>> ObterPorIdAsync(int id)
        {
            var jogo = await _unitOfWork.JogoRepository.ObterPorIdAsync(id);

            if (jogo == null)
                return Result.NotFound("Jogo não localizado.");

            return jogo.ToDTO();
        }

        public async Task<Result<IEnumerable<JogoDTO>>> ObterJogosDoUsuario(int usuarioId)
        {
            var usuario = await _unitOfWork.UsuarioRepository.ObterComBibliotecaAsync(usuarioId);

            if (usuario == null)
                return Result.NotFound("Usuário não localizado.");

            return Result.Success(usuario.Biblioteca.Select(j => j.ToDTO()));
        }

        public async Task<Result<JogoDTO>> CadastrarAsync(CadastrarJogoDTO dto)
        {
            if (!TryValidate(dto, out var validationResult))
                return validationResult;

            if (await ExisteJogoComNomeAsync(dto.Nome))
                return Result.Conflict("Já existe um jogo cadastrado com esse nome.");

            var entidade = dto.ToEntity();

            _unitOfWork.JogoRepository.Cadastrar(entidade);
            await _unitOfWork.CommitAsync();

            return entidade.ToDTO();
        }

        public async Task<Result<JogoDTO>> AlterarAsync(AlterarJogoDTO dto)
        {
            if (!TryValidate(dto, out var validationResult))
                return validationResult;

            var jogo = await _unitOfWork.JogoRepository.ObterPorIdAsync(dto.Id);

            if (jogo == null)
                return Result.NotFound("Jogo não localizado.");
            if (jogo.Nome != dto.Nome && await ExisteJogoComNomeAsync(dto.Nome))
                return Result.Conflict("Já existe um jogo cadastrado com esse nome.");

            var entidade = dto.ToEntity(jogo);

            _unitOfWork.JogoRepository.Cadastrar(entidade);
            await _unitOfWork.CommitAsync();

            return entidade.ToDTO();
        }

        public async Task<Result> DeletarAsync(int id)
        {
            var jogo = await _unitOfWork.JogoRepository.ObterPorIdAsync(id);

            if (jogo == null)
                return Result.NotFound("Jogo não localizado.");

            _unitOfWork.JogoRepository.Deletar(jogo);
            await _unitOfWork.CommitAsync();

            return Result.Success();
        }

        public void Dispose() => _unitOfWork.Dispose();

        private async Task<bool> ExisteJogoComNomeAsync(string nome) =>
            await _unitOfWork.JogoRepository.ObterPorNomeAsync(nome) != null;
    }
}
