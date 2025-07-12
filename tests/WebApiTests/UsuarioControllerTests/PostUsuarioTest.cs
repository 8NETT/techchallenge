using Ardalis.Result;
using FIAP.FCG.Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace WebApiTests.UsuarioControllerTests
{
    public class PostUsuarioTest : IClassFixture<UsuarioControllerFixture>
    {
        private UsuarioControllerFixture _fixture;

        public PostUsuarioTest(UsuarioControllerFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task CadastrarUsuario_OkResult()
        {
            // Act
            var input = new CadastrarUsuarioDTO
            {
                Nome = "Teste",
                Email = "teste@example.com",
                Password = "teste@1234",
                Profile = true
            };
            var output = new UsuarioDTO
            {
                Id = 1,
                DataCriacao = DateTime.Now,
                Nome = input.Nome,
                Email = input.Email,
                Profile = input.Profile
            };

            _fixture.UsuarioServiceMock.Setup(s => s.CadastrarAsync(input)).ReturnsAsync(output);

            // Act
            var result = await _fixture.Controller.Post(input);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task CadastrarUsuario_InvalidResult()
        {
            //Arrange
            var dto = new CadastrarUsuarioDTO
            {
                Nome = "Teste",
                Email = "teste@example.com",
                Password = "1234",
                Profile = true
            };

            _fixture.UsuarioServiceMock.Setup(s => s.CadastrarAsync(dto)).ReturnsAsync(Result.Invalid());

            // Act
            var result = await _fixture.Controller.Post(dto);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task CadastrarUsuario_ConflictResult()
        {
            //Arrange
            var dto = new CadastrarUsuarioDTO
            {
                Nome = "Teste",
                Email = "teste@example.com",
                Password = "teste@1234",
                Profile = true
            };

            _fixture.UsuarioServiceMock.Setup(s => s.CadastrarAsync(dto)).ReturnsAsync(Result.Conflict());

            // Act
            var result = await _fixture.Controller.Post(dto);

            // Assert
            Assert.IsType<ConflictObjectResult>(result);
        }
    }
}
