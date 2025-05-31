using Ardalis.Result;
using FIAP.FCG.Application.DTOs;
using FIAP.FCG.Core.Entity;
using Moq;

namespace ApplicationTests.JogoServiceTests
{
    public class CadastrarJogoTest : IClassFixture<JogoServiceFixture>
    {
        private JogoServiceFixture _fixture;

        public CadastrarJogoTest(JogoServiceFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task CadastrarJogo_SuccessResult()
        {
            // Arrange
            var dto = new CadastrarJogoDTO
            {
                Nome = "Teste",
                Valor = 100M,
                Desconto = 0
            };

            _fixture.UnitOfWorkMock.Setup(u => u.JogoRepository.ObterPorNomeAsync(dto.Nome)).ReturnsAsync(value: null);
            _fixture.UnitOfWorkMock.Setup(u => u.JogoRepository.Cadastrar(It.IsAny<Jogo>()));
            _fixture.UnitOfWorkMock.Setup(u => u.CommitAsync());

            // Act
            var result = await _fixture.Service.CadastrarAsync(dto);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(dto.Nome, result.Value.Nome);
            Assert.Equal(dto.Valor, result.Value.Valor);
            Assert.Equal(dto.Desconto, result.Value.Desconto);
        }

        [Fact]
        public async Task CadastrarJogo_InvalidResult_NomeVazio()
        {
            // Arrange
            var dto = new CadastrarJogoDTO
            {
                Nome = string.Empty,
                Valor = 100M,
                Desconto = 0
            };

            // Act
            var result = await _fixture.Service.CadastrarAsync(dto);

            // Assert
            Assert.True(result.IsInvalid());
        }

        [Fact]
        public async Task CadastrarJogo_InvalidResult_ValorNegativo()
        {
            // Arrange
            var dto = new CadastrarJogoDTO
            {
                Nome = "Teste",
                Valor = -100M,
                Desconto = 0
            };

            // Act
            var result = await _fixture.Service.CadastrarAsync(dto);

            // Assert
            Assert.True(result.IsInvalid());
        }

        [Fact]
        public async Task CadastrarJogo_InvalidResult_DescontoNegativo()
        {
            // Arrange
            var dto = new CadastrarJogoDTO
            {
                Nome = "Teste",
                Valor = 100M,
                Desconto = -10
            };

            // Act
            var result = await _fixture.Service.CadastrarAsync(dto);

            // Assert
            Assert.True(result.IsInvalid());
        }

        [Fact]
        public async Task CadastrarJogo_InvalidResult_DescontoMaiorQue100()
        {
            // Arrange
            var dto = new CadastrarJogoDTO
            {
                Nome = "Teste",
                Valor = 100M,
                Desconto = 200
            };

            // Act
            var result = await _fixture.Service.CadastrarAsync(dto);

            // Assert
            Assert.True(result.IsInvalid());
        }

        [Fact]
        public async Task CadastrarJogo_ConflictResult()
        {
            // Arrange
            var dto = new CadastrarJogoDTO
            {
                Nome = "Teste",
                Valor = 100M,
                Desconto = 0
            };
            var jogo = new Jogo
            {
                Id = 1,
                DataCriacao = DateTime.Now,
                Nome = "Teste",
                Valor = 100
            };

            _fixture.UnitOfWorkMock.Setup(u => u.JogoRepository.ObterPorNomeAsync(dto.Nome)).ReturnsAsync(jogo);

            // Act
            var result = await _fixture.Service.CadastrarAsync(dto);

            // Assert
            Assert.True(result.IsConflict());
        }
    }
}
