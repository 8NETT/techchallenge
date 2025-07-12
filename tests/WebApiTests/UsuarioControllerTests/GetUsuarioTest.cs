using Ardalis.Result;
using FIAP.FCG.Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace WebApiTests.UsuarioControllerTests
{
    public class GetUsuarioTest : IClassFixture<UsuarioControllerFixture>
    {
        private UsuarioControllerFixture _fixture;

        public GetUsuarioTest(UsuarioControllerFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task ObterTodosUsuarios_OkResult()
        {
            // Arrange
            _fixture.UsuarioServiceMock.Setup(s => s.ObterTodosAsync()).ReturnsAsync([]);

            // Act
            var result = await _fixture.Controller.Get();

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task ObterUsuarioPorId_OkResult()
        {
            // Arrange
            var dto = new UsuarioDTO
            {
                Id = 1,
                DataCriacao = DateTime.Now,
                Nome = "Teste",
                Email = "teste@example.com",
                Profile = true
            };
            _fixture.UsuarioServiceMock.Setup(s => s.ObterPorIdAsync(1)).ReturnsAsync(dto);

            // Act
            var result = await _fixture.Controller.Get(1);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task ObterUsuarioPorId_NotFoundResult()
        {
            // Arrange
            _fixture.UsuarioServiceMock.Setup(s => s.ObterPorIdAsync(1)).ReturnsAsync(Result.NotFound());

            // Act
            var result = await _fixture.Controller.Get(1);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }
    }
}
