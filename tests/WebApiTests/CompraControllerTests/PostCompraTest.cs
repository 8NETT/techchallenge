using Ardalis.Result;
using FIAP.FCG.WebApi.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Security.Claims;

namespace WebApiTests.CompraControllerTests
{
    public class PostCompraTest : IClassFixture<CompraControllerFixture>
    {
        private CompraControllerFixture _fixture;

        public PostCompraTest(CompraControllerFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task Comprar_OkResult()
        {
            // Arrange
            _fixture.CompraServiceMock.Setup(s => s.ComprarAsync(1, 1)).ReturnsAsync(Result.Success());
            ConfigurarClaimsDeAutenticacao(_fixture.Controller);

            // Act
            var result = await _fixture.Controller.Post(1);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task Comprar_NotFoundResult()
        {
            // Arrange
            _fixture.CompraServiceMock.Setup(s => s.ComprarAsync(1, 1)).ReturnsAsync(Result.NotFound());
            ConfigurarClaimsDeAutenticacao(_fixture.Controller);

            // Act
            var result = await _fixture.Controller.Post(1);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task Comprar_ConflictResult()
        {
            // Arrange
            _fixture.CompraServiceMock.Setup(s => s.ComprarAsync(1, 1)).ReturnsAsync(Result.Conflict());
            ConfigurarClaimsDeAutenticacao(_fixture.Controller);

            // Act
            var result = await _fixture.Controller.Post(1);

            // Assert
            Assert.IsType<ConflictObjectResult>(result);
        }

        [Fact]
        public async Task Estornar_OkResult()
        {
            // Arrange
            _fixture.CompraServiceMock.Setup(s => s.EstornarAsync(1)).ReturnsAsync(Result.Success());

            // Act
            var result = await _fixture.Controller.Estornar(1);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task Estornar_NotFoundResult()
        {
            // Arrange
            _fixture.CompraServiceMock.Setup(s => s.EstornarAsync(1)).ReturnsAsync(Result.NotFound());

            // Act
            var result = await _fixture.Controller.Estornar(1);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        private void ConfigurarClaimsDeAutenticacao(CompraController controller)
        {
            controller.ControllerContext.HttpContext = new DefaultHttpContext
            {
                User = new ClaimsPrincipal(new ClaimsIdentity([new Claim(ClaimTypes.NameIdentifier, "1")], "Fake Auth"))
            };
        }
    }
}
