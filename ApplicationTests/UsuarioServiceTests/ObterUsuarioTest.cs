using Ardalis.Result;
using FIAP.FCG.Core.Entity;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationTests.UsuarioServiceTests
{
    public class ObterUsuarioTest : IClassFixture<UsuarioServiceFixture>
    {
        private UsuarioServiceFixture _fixture;

        public ObterUsuarioTest(UsuarioServiceFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task ObterTodosUsuarios_SuccessResult()
        {
            // Arrange
            _fixture.UnitOfWorkMock.Setup(u => u.UsuarioRepository.ObterTodosAsync()).ReturnsAsync([]);

            // Act
            var usuarios = await _fixture.Service.ObterTodosAsync();

            // Assert
            Assert.Empty(usuarios);
        }

        [Fact]
        public async Task ObterPorId_SuccessResult()
        {
            // Arrange
            var usuario = new Usuario
            {
                Id = 1,
                DataCriacao = DateTime.Now,
                Nome = "Teste",
                Email = "teste@example.com",
                Password = "hashedPassword",
                Profile = true
            };

            _fixture.UnitOfWorkMock.Setup(u => u.UsuarioRepository.ObterPorIdAsync(1)).ReturnsAsync(usuario);

            // Act
            var result = await _fixture.Service.ObterPorIdAsync(1);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(usuario.Id, result.Value.Id);
            Assert.Equal(usuario.DataCriacao, result.Value.DataCriacao);
            Assert.Equal(usuario.Nome, result.Value.Nome);
            Assert.Equal(usuario.Email, result.Value.Email);
            Assert.Equal(usuario.Profile, result.Value.Profile);
        }

        [Fact]
        public async Task ObterPorId_NotFoundResult()
        {
            // Arrange
            _fixture.UnitOfWorkMock.Setup(u => u.UsuarioRepository.ObterPorIdAsync(1)).ReturnsAsync(value: null);

            // Act
            var result = await _fixture.Service.ObterPorIdAsync(1);

            // Assert
            Assert.True(result.IsNotFound());
        }
    }
}
