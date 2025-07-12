using Ardalis.Result;
using FIAP.FCG.Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;

namespace WebApiTests.UsuarioControllerTests
{
    public class PutUsuarioTest : IClassFixture<UsuarioControllerFixture>
    {
        private UsuarioControllerFixture _fixture;

        public PutUsuarioTest(UsuarioControllerFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task AlterarUsuario_OkResult()
        {
            // Arrange
            var input = new AlterarUsuarioDTO
            {
                Id = 1,
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

            _fixture.UsuarioServiceMock.Setup(s => s.AlterarAsync(input)).ReturnsAsync(output);

            // Act
            var result = await _fixture.Controller.Put(input);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task AlterarUsuario_InvalidResult()
        {
            // Arrange
            var dto = new AlterarUsuarioDTO
            {
                Id = 1,
                Nome = "Teste",
                Email = "teste@example.com",
                Password = "teste@1234",
                Profile = true
            };

            _fixture.UsuarioServiceMock.Setup(s => s.AlterarAsync(dto)).ReturnsAsync(Result.Invalid());

            // Act
            var result = await _fixture.Controller.Put(dto);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task AlterarUsuario_NotFoundResult()
        {
            // Arrange
            var dto = new AlterarUsuarioDTO
            {
                Id = 1,
                Nome = "Teste",
                Email = "teste@example.com",
                Password = "teste@1234",
                Profile = true
            };

            _fixture.UsuarioServiceMock.Setup(s => s.AlterarAsync(dto)).ReturnsAsync(Result.NotFound());

            // Act
            var result = await _fixture.Controller.Put(dto);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task AlterarUsuario_ConflictResult()
        {
            // Arrange
            var dto = new AlterarUsuarioDTO
            {
                Id = 1,
                Nome = "Teste",
                Email = "teste@example.com",
                Password = "teste@1234",
                Profile = true
            };

            _fixture.UsuarioServiceMock.Setup(s => s.AlterarAsync(dto)).ReturnsAsync(Result.Conflict());

            // Act
            var result = await _fixture.Controller.Put(dto);

            // Assert
            Assert.IsType<ConflictObjectResult>(result);
        }
    }
}
