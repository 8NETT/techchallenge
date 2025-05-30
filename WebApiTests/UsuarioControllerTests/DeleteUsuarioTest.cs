using Ardalis.Result;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiTests.UsuarioControllerTests
{
    public class DeleteUsuarioTest : IClassFixture<UsuarioControllerFixture>
    {
        private UsuarioControllerFixture _fixture;

        public DeleteUsuarioTest(UsuarioControllerFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task DeletarUsuario_OkResult()
        {
            // Arrange
            _fixture.UsuarioServiceMock.Setup(s => s.DeletarAsync(1)).ReturnsAsync(Result.Success());

            // Act
            var result = await _fixture.Controller.Delete(1);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task DeletarUsuario_NotFoundResult()
        {
            // Arrange
            _fixture.UsuarioServiceMock.Setup(s => s.DeletarAsync(1)).ReturnsAsync(Result.NotFound());

            // Act
            var result = await _fixture.Controller.Delete(1);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }
    }
}
