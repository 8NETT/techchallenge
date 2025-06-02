using Ardalis.Result;
using FIAP.FCG.Core.Entity;
using Moq;

namespace ApplicationTests.JogoServiceTests
{
    public class DeletarJogoTest : IClassFixture<JogoServiceFixture>
    {
        private JogoServiceFixture _fixture;

        public DeletarJogoTest(JogoServiceFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task DeletarJogo_SuccessResult()
        {
            // Arrange
            var jogo = Jogo.New()
                .Id(1)
                .DataCriacao(DateTime.Now)
                .Nome("Teste")
                .Valor(100M)
                .Desconto(0)
                .Build();

            _fixture.UnitOfWorkMock.Setup(u => u.JogoRepository.ObterPorIdAsync(1)).ReturnsAsync(jogo);
            _fixture.UnitOfWorkMock.Setup(u => u.JogoRepository.Deletar(jogo));
            _fixture.UnitOfWorkMock.Setup(u => u.CommitAsync());

            // Act
            var result = await _fixture.Service.DeletarAsync(1);

            // Assert
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task DeletarJogo_NotFoundResult()
        {
            // Arrange
            _fixture.UnitOfWorkMock.Setup(u => u.JogoRepository.ObterPorIdAsync(1)).ReturnsAsync(value: null);

            // Act
            var result = await _fixture.Service.DeletarAsync(1);

            // Assert
            Assert.True(result.IsNotFound());
        }
    }
}
