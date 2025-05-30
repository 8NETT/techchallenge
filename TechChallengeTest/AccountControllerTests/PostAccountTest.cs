using Core.Entity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TechChallenge.Models;

namespace TechChallengeTest.AccountControllerTests
{
    public class PostAccountTest : BaseTest
    {
        public PostAccountTest() : base() { }

        [Fact]
        public async Task Post_LoginUsuarioExistente_OkObjectResult()
        {
            //Arrange
            var input = new LoginModel { Email = "teste@gmail.com", Password = "teste123@" };
            var usuario = new Usuario { Nome = "Teste", Email = "teste@gmail.com", Password = "Hashedteste123@", Profile = false };

            UnitOfWork.Setup(u => u.UsuarioRepository.ObterPorEmailAsync(input.Email)).ReturnsAsync(usuario);
            PasswordHasherMock.Setup(p => p.Verify(input.Password, usuario.Password)).Returns(true);
            JwtServiceMock.Setup(j => j.GenerateToken(usuario)).Returns("token-falso");

            //Act
            var result = await Controller.Login(input);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task Post_UsuarioInexistente_Unauthorized()
        {
            //Arrange

            var input = new LoginModel { Email = "teste@gmail.com", Password = "teste123@" };

            UnitOfWork.Setup(u => u.UsuarioRepository.ObterPorEmailAsync(input.Email)).ReturnsAsync(value: null);

            //Act
            var result = await Controller.Login(input);

            // Assert
            Assert.IsType<UnauthorizedResult>(result);
        }

        [Fact]
        public async Task Post_UsuarioIncorreto_Unauthorized()
        {
            //Arrange
            var input = new LoginModel { Email = "teste@gmail.com", Password = "teste123@" };
            var usuario = new Usuario { Nome = "Teste", Email = "teste@gmail.com", Password = "teste123@", Profile = false };

            UnitOfWork.Setup(u => u.UsuarioRepository.ObterPorEmailAsync(input.Email)).ReturnsAsync(usuario);
            PasswordHasherMock.Setup(p => p.Verify(input.Password, usuario.Password)).Returns(false);

            //Act
            var result = await Controller.Login(input);

            // Assert
            Assert.IsType<UnauthorizedResult>(result);
        }

        [Fact]
        public async Task Post_CampoVazioSenha_BadRequestResult()
        {
            //Arrange

            var input = new LoginModel { Email = "teste@gmail.com", Password = "" };
            var usuario = new Usuario { Nome = "Teste", Email = "teste@gmail.com", Password = "teste123@", Profile = false };

            UnitOfWork.Setup(u => u.UsuarioRepository.ObterPorEmailAsync(input.Email)).ReturnsAsync(usuario);
            PasswordHasherMock.Setup(p => p.Verify(input.Password, usuario.Password)).Returns(false);
            JwtServiceMock.Setup(j => j.GenerateToken(usuario)).Returns("token-falso");
            Controller.ModelState.AddModelError("Password", "O campo Password precisa ser preenchido.");

            //Act
            var result = await Controller.Login(input);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }
    }
}
