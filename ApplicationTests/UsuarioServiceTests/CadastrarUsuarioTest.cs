using Ardalis.Result;
using FIAP.FCG.Application.DTOs;
using FIAP.FCG.Core.Entity;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationTests.UsuarioServiceTests
{
    public class CadastrarUsuarioTest : IClassFixture<UsuarioServiceFixture>
    {
        private UsuarioServiceFixture _fixture;

        public CadastrarUsuarioTest(UsuarioServiceFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task CadastrarUsuario_SuccessResult()
        {
            // Arrange
            var dto = new CadastrarUsuarioDTO
            {
                Nome = "Teste",
                Email = "teste@example.com",
                Password = "teste@1234",
                Profile = true
            };

            _fixture.UnitOfWorkMock.Setup(u => u.UsuarioRepository.ObterPorEmailAsync(dto.Email)).ReturnsAsync(value: null);
            _fixture.UnitOfWorkMock.Setup(u => u.UsuarioRepository.Cadastrar(It.IsAny<Usuario>()));
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
        public async Task CadastrarUsuario_InvalidResult_NomeVazio()
        {
            // Arrange
            var dto = new CadastrarUsuarioDTO
            {
                Nome = string.Empty,
                Email = "teste@example.com",
                Password = "teste@1234",
                Profile = true
            };

            // Act
            var result = await _fixture.Service.CadastrarAsync(dto);

            // Assert
            Assert.True(result.IsInvalid());
        }

        [Fact]
        public async Task CadastrarUsuario_InvalidResult_EmailVazio()
        {
            // Arrange
            var dto = new CadastrarUsuarioDTO
            {
                Nome = "Teste",
                Email = string.Empty,
                Password = "teste@1234",
                Profile = true
            };

            // Act
            var result = await _fixture.Service.CadastrarAsync(dto);

            // Assert
            Assert.True(result.IsInvalid());
        }

        [Fact]
        public async Task CadastrarUsuario_InvalidResult_SenhaVazia()
        {
            // Arrange
            var dto = new CadastrarUsuarioDTO
            {
                Nome = "Teste",
                Email = "teste@example.com",
                Password = string.Empty,
                Profile = true
            };

            // Act
            var result = await _fixture.Service.CadastrarAsync(dto);

            // Assert
            Assert.True(result.IsInvalid());
        }

        [Fact]
        public async Task CadastrarUsuario_InvalidResult_SenhaFraca()
        {
            // Arrange
            var dto = new CadastrarUsuarioDTO
            {
                Nome = "Teste",
                Email = "teste@example.com",
                Password = "teste1234",
                Profile = true
            };

            // Act
            var result = await _fixture.Service.CadastrarAsync(dto);

            // Assert
            Assert.True(result.IsInvalid());
        }

        [Fact]
        public async Task CadastrarUsuario_ConflictResult()
        {
            // Arrange
            var dto = new CadastrarUsuarioDTO
            {
                Nome = "Teste",
                Email = "teste@example.com",
                Password = "teste@1234",
                Profile = true
            };
            var usuario = new Usuario
            {
                Id = 1,
                DataCriacao = DateTime.Now,
                Nome = "Outro Teste",
                Email = "teste@example.com",
                Password = "hashedPassword",
                Profile = true
            };

            _fixture.UnitOfWorkMock.Setup(u => u.UsuarioRepository.ObterPorEmailAsync(dto.Email)).ReturnsAsync(usuario);

            // Act
            var result = await _fixture.Service.CadastrarAsync(dto);

            // Assert
            Assert.True(result.IsConflict());
        }
    }
}
