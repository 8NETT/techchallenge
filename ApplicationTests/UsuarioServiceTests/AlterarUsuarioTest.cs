using Ardalis.Result;
using FIAP.FCG.Application.DTOs;
using FIAP.FCG.Core.Entity;
using Microsoft.Identity.Client;
using Moq;

namespace ApplicationTests.UsuarioServiceTests
{
    public class AlterarUsuarioTest : IClassFixture<UsuarioServiceFixture>
    {
        private UsuarioServiceFixture _fixture;

        public AlterarUsuarioTest(UsuarioServiceFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task AlterarUsuario_SuccessResult()
        {
            // Arrange
            var dto = new AlterarUsuarioDTO
            {
                Id = 1,
                Nome = "Novo Teste",
                Email = "teste@example.com",
                Password = "teste@1234",
                Profile = true
            };
            var usuario = new Usuario
            {
                Id = 1,
                DataCriacao = DateTime.Now,
                Nome = "Teste",
                Email = "teste@example.com",
                Password = "hashedPassword",
                Profile = true
            };

            _fixture.UnitOfWorkMock.Setup(u => u.UsuarioRepository.ObterPorIdAsync(dto.Id)).ReturnsAsync(usuario);
            _fixture.UnitOfWorkMock.Setup(u => u.UsuarioRepository.Cadastrar(usuario));
            _fixture.UnitOfWorkMock.Setup(u => u.CommitAsync());
            _fixture.PasswordHasherMock.Setup(p => p.Hash(dto.Password)).Returns("hashedPassword");

            // Act
            var result = await _fixture.Service.CadastrarAsync(dto);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(dto.Nome, result.Value.Nome);
            Assert.Equal(dto.Email, result.Value.Email);
            Assert.Equal(dto.Profile, result.Value.Profile);
        }

        [Fact]
        public async Task AlterarUsuario_NotFoundResult()
        {
            // Arrange
            var dto = new AlterarUsuarioDTO
            {
                Id = 1,
                Nome = "Novo Teste",
                Email = "teste@example.com",
                Password = "teste@1234",
                Profile = true
            };

            _fixture.UnitOfWorkMock.Setup(u => u.UsuarioRepository.ObterPorIdAsync(dto.Id)).ReturnsAsync(value: null);

            // Act
            var result = await _fixture.Service.AlterarAsync(dto);

            // Assert
            Assert.True(result.IsNotFound());
        }

        [Fact]
        public async Task AlterarUsuario_ConflictResult()
        {
            // Arrange
            var dto = new AlterarUsuarioDTO
            {
                Id = 1,
                Nome = "Teste",
                Email = "outroteste@example.com",
                Password = "teste@1234",
                Profile = true
            };
            var usuario = new Usuario
            {
                Id = 1,
                DataCriacao = DateTime.Now,
                Nome = "Teste",
                Email = "teste@example.com",
                Password = "hashedPassword",
                Profile = true
            };
            var outroUsuario = new Usuario
            {
                Id = 2,
                DataCriacao = DateTime.Now,
                Nome = "Outro Teste",
                Email = "outroteste@example.com",
                Password = "hashedPassword",
                Profile = true
            };

            _fixture.UnitOfWorkMock.Setup(u => u.UsuarioRepository.ObterPorIdAsync(dto.Id)).ReturnsAsync(usuario);
            _fixture.UnitOfWorkMock.Setup(u => u.UsuarioRepository.ObterPorEmailAsync(dto.Email)).ReturnsAsync(outroUsuario);

            // Act
            var result = await _fixture.Service.AlterarAsync(dto);

            // Assert
            Assert.True(result.IsConflict());
        }

        [Fact]
        public async Task AlterarUsuario_InvalidResult_NomeVazio()
        {
            // Arrange
            var dto = new AlterarUsuarioDTO
            {
                Id = 1,
                Nome = string.Empty,
                Email = "teste@example.com",
                Password = "teste@1234",
                Profile = true
            };

            // Act
            var result = await _fixture.Service.AlterarAsync(dto);

            // Assert
            Assert.True(result.IsInvalid());
        }

        [Fact]
        public async Task AlterarUsuario_InvalidResult_EmailVazio()
        {
            // Arrange
            var dto = new AlterarUsuarioDTO
            {
                Id = 1,
                Nome = "Teste",
                Email = string.Empty,
                Password = "teste@1234",
                Profile = true
            };

            // Act
            var result = await _fixture.Service.AlterarAsync(dto);

            // Assert
            Assert.True(result.IsInvalid());
        }

        [Fact]
        public async Task AlterarUsuario_InvalidResult_SenhaVazia()
        {
            // Arrange
            var dto = new AlterarUsuarioDTO
            {
                Id = 1,
                Nome = "Teste",
                Email = "teste@example.com",
                Password = string.Empty,
                Profile = true
            };

            // Act
            var result = await _fixture.Service.AlterarAsync(dto);

            // Assert
            Assert.True(result.IsInvalid());
        }

        [Fact]
        public async Task AlterarUsuario_InvalidResult_SenhaFraca()
        {
            // Arrange
            var dto = new AlterarUsuarioDTO
            {
                Id = 1,
                Nome = "Teste",
                Email = "teste@example.com",
                Password = "teste1234",
                Profile = true
            };

            // Act
            var result = await _fixture.Service.AlterarAsync(dto);

            // Assert
            Assert.True(result.IsInvalid());
        }
    }
}
