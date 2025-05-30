using Ardalis.Result;
using FIAP.FCG.Core.Entity;
using Moq;

namespace ApplicationTests.UsuarioServiceTests
{
    public class DeletarUsuarioTest : IClassFixture<UsuarioServiceFixture>
    {
        private UsuarioServiceFixture _fixture;

        public DeletarUsuarioTest(UsuarioServiceFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task DeletarUsuario_SuccessResult()
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
            _fixture.UnitOfWorkMock.Setup(u => u.UsuarioRepository.Deletar(usuario));
            _fixture.UnitOfWorkMock.Setup(u => u.CommitAsync());

            // Act
            var result = await _fixture.Service.DeletarAsync(1);

            // Assert
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task DeletarUsuario_NotFoundResult()
        {
            // Arrange
            _fixture.UnitOfWorkMock.Setup(u => u.UsuarioRepository.ObterPorIdAsync(1)).ReturnsAsync(value: null);

            // Act
            var result = await _fixture.Service.DeletarAsync(1);

            // Assert
            Assert.True(result.IsNotFound());
        }
    }
}
