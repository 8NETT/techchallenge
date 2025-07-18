﻿using Ardalis.Result;
using FIAP.FCG.Application.DTOs;

namespace FIAP.FCG.Application.Contracts
{
    public interface IJogoService : IDisposable
    {
        Task<IEnumerable<JogoDTO>> ObterTodosAsync();
        Task<Result<JogoDTO>> ObterPorIdAsync(int id);
        Task<Result<IEnumerable<JogoDTO>>> ObterJogosDoUsuario(int usuarioId);
        Task<Result<JogoDTO>> CadastrarAsync(CadastrarJogoDTO dto);
        Task<Result<JogoDTO>> AlterarAsync(AlterarJogoDTO dto);
        Task<Result> DeletarAsync(int id);
    }
}
