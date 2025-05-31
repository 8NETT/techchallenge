using Ardalis.Result;
using FIAP.FCG.Core.Entity;
using Moq;

namespace ApplicationTests.JogoServiceTests
{
    public class ObterJogoTest : IClassFixture<JogoServiceFixture>
    {
        private JogoServiceFixture _fixture;

        public ObterJogoTest(JogoServiceFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task ObterTodos_SuccessResult()
        {
            // Arrange
            _fixture.UnitOfWorkMock.Setup(u => u.JogoRepository.ObterTodosAsync()).ReturnsAsync([]);

            // Act
            var jogos = await _fixture.Service.ObterTodosAsync();

            // Assert
            Assert.Empty(jogos);
        }

        [Fact]
        public async Task ObterPorId_SuccessResult()
        {
            // Arrange
            var jogo = new Jogo
            {
                Id = 1,
                DataCriacao = DateTime.Now,
                Nome = "Teste",
                Valor = 100M,
                Desconto = 0
            };

            _fixture.UnitOfWorkMock.Setup(u => u.JogoRepository.ObterPorIdAsync(1)).ReturnsAsync(jogo);

            // Act
            var result = await _fixture.Service.ObterPorIdAsync(1);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(jogo.Id, result.Value.Id);
            Assert.Equal(jogo.Nome, result.Value.Nome);
            Assert.Equal(jogo.Valor, result.Value.Valor);
            Assert.Equal(jogo.Desconto, result.Value.Desconto);
        }

        [Fact]
        public async Task ObterPorId_NotFoundResult()
        {
            // Arrange
            _fixture.UnitOfWorkMock.Setup(u => u.JogoRepository.ObterPorIdAsync(1)).ReturnsAsync(value: null);

            // Act
            var result = await _fixture.Service.ObterPorIdAsync(1);

            // Assert
            Assert.True(result.IsNotFound());
        }
    }
}
