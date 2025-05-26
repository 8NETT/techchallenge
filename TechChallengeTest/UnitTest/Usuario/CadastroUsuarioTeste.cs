using Core.Input;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TechChallenge.Controllers;
using TechChallengeTest.UnitTest.Usuario.Utils;

namespace TechChallengeTest.UnitTest.Usuario
{
    public class CadastroUsuarioTeste : IClassFixture<UsuarioControllerFixture>
    {

        private readonly UsuarioControllerFixture _usuarioControllerFixture;

        public CadastroUsuarioTeste(UsuarioControllerFixture usuarioControllerFixture)
        {
            _usuarioControllerFixture = usuarioControllerFixture;
        }


        [Fact]
        public void Post_DeveCadastrarUsuarioComSucesso_OkResult()
        {
            // Arrange
            var input = new UsuarioInput { Nome = "Teste", Email = "teste@teste.com", Password = "Senha@123" };

            _usuarioControllerFixture.PasswordHasherMock.Setup(h => h.Hash(It.IsAny<string>())).Returns("senhaHash");

            var controller = new UsuarioController(
                _usuarioControllerFixture.UsuarioRepositoryMock.Object,
                _usuarioControllerFixture.PasswordHasherMock.Object,
                _usuarioControllerFixture.JwtServiceMock.Object);

            // Act
            var result = controller.Post(input);

            // Assert
            Assert.IsType<OkResult>(result);

        }

        [Fact]
        public void Post_NaoDeveCadastrarUsuario_BadRequest()
        {
            //Arrange
            var input = new UsuarioInput { Nome = "teste", Email = "teste", Password = "Senha@123" };

            _usuarioControllerFixture.PasswordHasherMock.Setup(h => h.Hash(It.IsAny<string>())).Returns("senhaHash");

            var controller = new UsuarioController(
                _usuarioControllerFixture.UsuarioRepositoryMock.Object,
                _usuarioControllerFixture.PasswordHasherMock.Object,
                _usuarioControllerFixture.JwtServiceMock.Object);

            controller.ModelState.AddModelError("Email", "Email inválido");


            //Act
            var result = controller.Post(input);


            //Assert
            Assert.IsType<BadRequestObjectResult>(result);

        }



    }
}