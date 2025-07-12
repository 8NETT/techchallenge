using Ardalis.Result;
using FIAP.FCG.Application.Contracts;
using FIAP.FCG.Application.DTOs;
using FIAP.FCG.Application.Mappers;
using FIAP.FCG.Application.Security;
using FIAP.FCG.Core.Repository;

namespace FIAP.FCG.Application.Services
{
    public class UsuarioService : BaseService, IUsuarioService
    {
        private IUnitOfWork _unitOfWork;
        private IPasswordHasher _passwordHasher;

        public UsuarioService(IUnitOfWork unitOfWork, IPasswordHasher passwordHasher)
        {
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
        }

        public async Task<IEnumerable<UsuarioDTO>> ObterTodosAsync()
        {
            var usuarios = await _unitOfWork.UsuarioRepository.ObterTodosAsync();
            return usuarios.Select(u => u.ToDTO());
        }

        public async Task<Result<UsuarioDTO>> ObterPorIdAsync(int id)
        {
            var usuario = await _unitOfWork.UsuarioRepository.ObterPorIdAsync(id);

            if (usuario == null)
                return Result.NotFound("Usuário não localizado.");

            return usuario.ToDTO();
        }

        public async Task<Result<UsuarioDTO>> CadastrarAsync(CadastrarUsuarioDTO dto)
        {
            if (!TryValidate(dto, out var validationResult))
                return validationResult;

            if (await ExisteUsuarioComEmailAsync(dto.Email))
                return Result.Conflict("Já existe um usuário cadastrado com esse email.");

            var passwordHash = _passwordHasher.Hash(dto.Password);
            var entidade = dto.ToEntity(passwordHash);

            _unitOfWork.UsuarioRepository.Cadastrar(entidade);
            await _unitOfWork.CommitAsync();

            return entidade.ToDTO();
        }

        public async Task<Result<UsuarioDTO>> AlterarAsync(AlterarUsuarioDTO dto)
        {
            if (!TryValidate(dto, out var validationResult))
                return validationResult;

            var usuario = await _unitOfWork.UsuarioRepository.ObterPorIdAsync(dto.Id);

            if (usuario == null)
                return Result.NotFound("Usuário não localizado.");
            if (dto.Email != usuario.Email && await ExisteUsuarioComEmailAsync(dto.Email))
                return Result.Conflict("Já existe um usuário cadastrados com esse email.");

            var passwordHash = _passwordHasher.Hash(dto.Password);
            var entidade = dto.ToEntity(usuario, passwordHash);

            _unitOfWork.UsuarioRepository.Alterar(entidade);
            await _unitOfWork.CommitAsync();

            return entidade.ToDTO();
        }

        public async Task<Result> DeletarAsync(int id)
        {
            var usuario = await _unitOfWork.UsuarioRepository.ObterPorIdAsync(id);

            if (usuario == null)
                return Result.NotFound();

            _unitOfWork.UsuarioRepository.Deletar(usuario);
            await _unitOfWork.CommitAsync();

            return Result.Success();
        }

        public async Task<Result<UsuarioDTO>> LoginAsync(LoginDTO dto)
        {
            if (!TryValidate(dto, out var validationResult))
                return validationResult;

            var usuario = await _unitOfWork.UsuarioRepository.ObterPorEmailAsync(dto.Email);

            if (usuario == null)
                return Result.Unauthorized();
            if (!_passwordHasher.Verify(dto.Password, usuario.Password))
                return Result.Unauthorized();

            return usuario.ToDTO();
        }

        public void Dispose() => _unitOfWork.Dispose();

        private async Task<bool> ExisteUsuarioComEmailAsync(string email) =>
            await _unitOfWork.UsuarioRepository.ObterPorEmailAsync(email) != null;
    }
}
