using Core.Input;
using Core.Repository;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TechChallenge.Controllers;
using TechChallenge.Security;

namespace TechChallengeTest
{
    public class CadastroUsuarioTeste
    {
        [Fact]
        public void Post_DeveCadastrarUsuarioComSucesso()
        {
            // Arrange
            var input = new UsuarioInput {Nome = "Teste",Email = "teste@teste.com",Password = "Senha@123"};

            var mockRepo = new Mock<IUsuarioRepository>();
            var mockHasher = new Mock<IPasswordHasher>();
            var mockJwt = new Mock<IJwtService>();

            mockHasher.Setup(h => h.Hash(It.IsAny<string>())).Returns("senhaHash");//isola o hasher

            var controller = new UsuarioController(mockRepo.Object, mockHasher.Object, mockJwt.Object);

            // Act
            var result = controller.Post(input);

            // Assert
            Assert.IsType<OkResult>(result);

        }

        [Fact]
        public void Post_NaoDeveCadastrarUsuario_BadRequest()
        {
            //Arrange
            var input = new UsuarioInput{Nome = "teste", Email = "teste", Password = "Senha@123"};

            var mockRepo = new Mock<IUsuarioRepository>();
            var mockHasher = new Mock<IPasswordHasher>();
            var mockJwt = new Mock<IJwtService>();

            mockHasher.Setup(h => h.Hash(It.IsAny<string>())).Returns("senhaHash");//isola o hasher

            var controller = new UsuarioController(mockRepo.Object,mockHasher.Object, mockJwt.Object);
            controller.ModelState.AddModelError("Email", "Email inválido");



            //Act
            var result = controller.Post(input);


            //Assert
            Assert.IsType<BadRequestObjectResult>(result);

        }



    }
}