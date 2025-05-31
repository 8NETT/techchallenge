using Ardalis.Result;
using FIAP.FCG.Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace WebApiTests.JogoControllerTests
{
    public class PostJogoTest : IClassFixture<JogoControllerFixture>
    {
        private JogoControllerFixture _fixture;

        public PostJogoTest(JogoControllerFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task CadastrarJogo_OkResult()
        {
            // Arrange
            var input = new CadastrarJogoDTO
            {
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

            _fixture.JogoServiceMock.Setup(s => s.CadastrarAsync(input)).ReturnsAsync(output);

            // Act
            var result = await _fixture.Controller.Post(input);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task CadastrarJogo_InvalidResult()
        {
            //Arrange
            var dto = new CadastrarJogoDTO
            {
                Nome = string.Empty,
                Valor = 100M,
                Desconto = 0
            };

            _fixture.JogoServiceMock.Setup(s => s.CadastrarAsync(dto)).ReturnsAsync(Result.Invalid());

            // Act
            var result = await _fixture.Controller.Post(dto);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task CadastrarJogo_ConflictResult()
        {
            //Arrange
            var dto = new CadastrarJogoDTO
            {
                Nome = "Teste",
                Valor = 100M,
                Desconto = 0
            };

            _fixture.JogoServiceMock.Setup(s => s.CadastrarAsync(dto)).ReturnsAsync(Result.Conflict());

            // Act
            var result = await _fixture.Controller.Post(dto);

            // Assert
            Assert.IsType<ConflictObjectResult>(result);
        }
    }
}
