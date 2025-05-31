using Ardalis.Result;
using FIAP.FCG.Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace WebApiTests.JogoControllerTests
{
    public class GetJogoTest : IClassFixture<JogoControllerFixture>
    {
        private JogoControllerFixture _fixture;

        public GetJogoTest(JogoControllerFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task ObterTodos_OkResult()
        {
            // Arrange
            _fixture.JogoServiceMock.Setup(s => s.ObterTodosAsync()).ReturnsAsync([]);

            // Act
            var result = await _fixture.Controller.Get();

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task ObterPorId_OkResult()
        {
            // Arrange
            var jogo = new JogoDTO
            {
                Id = 1,
                Nome = "Tester",
                Valor = 100M,
                Desconto = 0
            };

            _fixture.JogoServiceMock.Setup(s => s.ObterPorIdAsync(1)).ReturnsAsync(jogo);

            // Act
            var result = await _fixture.Controller.Get(1);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task ObterPorId_NotFoundResult()
        {
            // Arrange
            _fixture.JogoServiceMock.Setup(s => s.ObterPorIdAsync(1)).ReturnsAsync(Result.NotFound());

            // Act
            var result = await _fixture.Controller.Get(1);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }
    }
}
