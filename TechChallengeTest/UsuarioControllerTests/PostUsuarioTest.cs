using Core.Entity;
using Core.Input;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TechChallengeTest.UsuarioControllerTests;

namespace TechChallengeTest.UnitTest.Usuarios
{
    public class PostUsuarioTest : BaseTest
    {
        public PostUsuarioTest() : base() { }

        [Fact]
        public async Task Post_DeveCadastrarUsuarioComSucesso_OkResult()
        {
            // Arrange
            var input = new UsuarioInput { Nome = "Teste", Email = "teste@teste.com", Password = "Senha@123" };
            UnitOfWorkMock.Setup(u => u.UsuarioRepository.Cadastrar(It.IsAny<Usuario>()));
            PasswordHasherMock.Setup(h => h.Hash(It.IsAny<string>())).Returns("senhaHash");

            // Act
            var result = await Controller.Post(input);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task Post_NaoDeveCadastrarUsuario_BadRequest()
        {
            //Arrange
            var input = new UsuarioInput { Nome = "teste", Email = "teste", Password = "Senha@123" };

            PasswordHasherMock.Setup(h => h.Hash(It.IsAny<string>())).Returns("senhaHash");
            Controller.ModelState.AddModelError("Email", "Email inválido");

            //Act
            var result = await Controller.Post(input);

            //Assert
            Assert.IsType<BadRequestResult>(result);
        }
    }
}