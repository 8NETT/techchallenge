using Ardalis.Result;
using FIAP.FCG.Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace WebApiTests.JogoControllerTests
{
    public class PutJogoTest : IClassFixture<JogoControllerFixture>
    {
        private JogoControllerFixture _fixture;

        public PutJogoTest(JogoControllerFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task AlterarJogo_OkResult()
        {
            // Arrange
            var input = new AlterarJogoDTO
            {
                Id = 1,
                Nome = "Teste",
                Valor = 100M,
                Desconto = 0
            };
            var output = new JogoDTO
            {
                Id = 1,
                Nome = input.Nome,
                Valor = input.Valor,
                Desconto = input.Desconto
            };

            _fixture.JogoServiceMock.Setup(s => s.AlterarAsync(input)).ReturnsAsync(output);

            // Act
            var result = await _fixture.Controller.Put(input);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task AlterarJogo_InvalidResult()
        {
            // Arrange
            var dto = new AlterarJogoDTO
            {
                Id = 1,
                Nome = string.Empty,
                Valor = 100M,
                Desconto = 0
            };

            _fixture.JogoServiceMock.Setup(s => s.AlterarAsync(dto)).ReturnsAsync(Result.Invalid());

            // Act
            var result = await _fixture.Controller.Put(dto);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task AlterarJogo_NotFoundResult()
        {
            // Arrange
            var dto = new AlterarJogoDTO
            {
                Id = 1,
                Nome = "Teste",
                Valor = 100M,
                Desconto = 0
            };

            _fixture.JogoServiceMock.Setup(s => s.AlterarAsync(dto)).ReturnsAsync(Result.NotFound());

            // Act
            var result = await _fixture.Controller.Put(dto);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task AlterarJogo_ConflictResult()
        {
            // Arrange
            var dto = new AlterarJogoDTO
            {
                Id = 1,
                Nome = "Teste",
                Valor = 100M,
                Desconto = 0
            };

            _fixture.JogoServiceMock.Setup(s => s.AlterarAsync(dto)).ReturnsAsync(Result.Conflict());

            // Act
            var result = await _fixture.Controller.Put(dto);

            // Assert
            Assert.IsType<ConflictObjectResult>(result);
        }
    }
}
