using Ardalis.Result;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace WebApiTests.JogoControllerTests
{
    public class DeleteJogoTest : IClassFixture<JogoControllerFixture>
    {
        private JogoControllerFixture _fixture;

        public DeleteJogoTest(JogoControllerFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task DeletarJogo_OkResult()
        {
            // Arrange
            _fixture.JogoServiceMock.Setup(s => s.DeletarAsync(1)).ReturnsAsync(Result.Success());

            // Act
            var result = await _fixture.Controller.Delete(1);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task DeletarJogo_NotFoundResult()
        {
            // Arrange
            _fixture.JogoServiceMock.Setup(s => s.DeletarAsync(1)).ReturnsAsync(Result.NotFound());

            // Act
            var result = await _fixture.Controller.Delete(1);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }
    }
}
