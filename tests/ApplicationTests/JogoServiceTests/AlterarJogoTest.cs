using Ardalis.Result;
using FIAP.FCG.Application.DTOs;
using FIAP.FCG.Core.Entity;
using Moq;

namespace ApplicationTests.JogoServiceTests
{
    public class AlterarJogoTest : IClassFixture<JogoServiceFixture>
    {
        private JogoServiceFixture _fixture;

        public AlterarJogoTest(JogoServiceFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task AlterarJogo_SuccessResult()
        {
            // Arrange
            var dto = new AlterarJogoDTO
            {
                Id = 1,
                Nome = "Novo Teste",
                Valor = 100M,
                Desconto = 10
            };
            var jogo = Jogo.New()
                .Id(1)
                .DataCriacao(DateTime.Now)
                .Nome("Teste")
                .Valor(50M)
                .Desconto(0)
                .Build();

            _fixture.UnitOfWorkMock.Setup(u => u.JogoRepository.ObterPorIdAsync(dto.Id)).ReturnsAsync(jogo);
            _fixture.UnitOfWorkMock.Setup(u => u.JogoRepository.Cadastrar(jogo));
            _fixture.UnitOfWorkMock.Setup(u => u.CommitAsync());

            // Act
            var result = await _fixture.Service.AlterarAsync(dto);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(dto.Id, result.Value.Id);
            Assert.Equal(dto.Nome, result.Value.Nome);
            Assert.Equal(dto.Valor, result.Value.Valor);
            Assert.Equal(dto.Desconto, result.Value.Desconto);
        }

        [Fact]
        public async Task AlterarJogo_NotFoundResult()
        {
            // Arrange
            var dto = new AlterarJogoDTO
            {
                Id = 1,
                Nome = "Novo Teste",
                Valor = 100M,
                Desconto = 0
            };

            _fixture.UnitOfWorkMock.Setup(u => u.JogoRepository.ObterPorIdAsync(dto.Id)).ReturnsAsync(value: null);

            // Act
            var result = await _fixture.Service.AlterarAsync(dto);

            // Assert
            Assert.True(result.IsNotFound());
        }

        [Fact]
        public async Task AlterarJogo_ConflictResult()
        {
            // Arrange
            var dto = new AlterarJogoDTO
            {
                Id = 1,
                Nome = "Outro Teste",
                Valor = 100M,
                Desconto = 0
            };
            var jogo = Jogo.New()
                .Id(1)
                .DataCriacao(DateTime.Now)
                .Nome("Teste")
                .Valor(100M)
                .Desconto(0)
                .Build();
            var outroJogo = Jogo.New()
                .Id(2)
                .DataCriacao(DateTime.Now)
                .Nome("Outro Teste")
                .Valor(50M)
                .Desconto(20)
                .Build();

            _fixture.UnitOfWorkMock.Setup(u => u.JogoRepository.ObterPorIdAsync(dto.Id)).ReturnsAsync(jogo);
            _fixture.UnitOfWorkMock.Setup(u => u.JogoRepository.ObterPorNomeAsync(dto.Nome)).ReturnsAsync(outroJogo);

            // Act
            var result = await _fixture.Service.AlterarAsync(dto);

            // Assert
            Assert.True(result.IsConflict());
        }

        [Fact]
        public async Task AlterarJogo_InvalidResult_NomeVazio()
        {
            // Arrange
            var dto = new AlterarJogoDTO
            {
                Id = 1,
                Nome = string.Empty,
                Valor = 100M,
                Desconto = 0
            };

            // Act
            var result = await _fixture.Service.AlterarAsync(dto);

            // Assert
            Assert.True(result.IsInvalid());
        }

        [Fact]
        public async Task AlterarJogo_InvalidResult_ValorNegativo()
        {
            // Arrange
            var dto = new AlterarJogoDTO
            {
                Id = 1,
                Nome = "Teste",
                Valor = -100M,
                Desconto = 0
            };

            // Act
            var result = await _fixture.Service.AlterarAsync(dto);

            // Assert
            Assert.True(result.IsInvalid());
        }

        [Fact]
        public async Task AlterarJogo_InvalidResult_DescontoNegativo()
        {
            // Arrange
            var dto = new AlterarJogoDTO
            {
                Id = 1,
                Nome = "Teste",
                Valor = 100M,
                Desconto = -10
            };

            // Act
            var result = await _fixture.Service.AlterarAsync(dto);

            // Assert
            Assert.True(result.IsInvalid());
        }

        [Fact]
        public async Task AlterarJogo_InvalidResult_DescontoMaiorQue100()
        {
            // Arrange
            var dto = new AlterarJogoDTO
            {
                Id = 1,
                Nome = "Teste",
                Valor = 100M,
                Desconto = 200
            };

            // Act
            var result = await _fixture.Service.AlterarAsync(dto);

            // Assert
            Assert.True(result.IsInvalid());
        }
    }
}
