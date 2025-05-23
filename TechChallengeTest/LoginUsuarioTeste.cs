using Core.Entity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TechChallenge.Models;

namespace TechChallengeTest
{
    public class LoginUsuarioTeste : IClassFixture<UsuarioControllerFixture>
    {
        private readonly UsuarioControllerFixture _usuarioControllerFixture;

        public LoginUsuarioTeste(UsuarioControllerFixture usuarioControllerFixture)
        {
            _usuarioControllerFixture = usuarioControllerFixture;
        }

        [Fact]
        public async Task Post_LoginUsuarioExistente_OkObjectResult()
        {
            //Arrange
            var input = new LoginModel { Email = "teste@gmail.com", Password = "teste123@" };
            var usuario = new Usuario {Nome="Teste", Email = "teste@gmail.com", Password = "Hashedteste123@",Profile=false };

            _usuarioControllerFixture.UsuarioRepositoryMock.Setup(r=>r.ObterPorEmailAsync(input.Email)).ReturnsAsync(usuario);
            _usuarioControllerFixture.PasswordHasherMock.Setup(p => p.Verify(input.Password, usuario.Password)).Returns(true);
            _usuarioControllerFixture.JwtServiceMock.Setup(j => j.GenerateToken(usuario)).Returns("token-falso");

       
            //Act
            var result = await _usuarioControllerFixture.Controller.Login(input);


            // Assert
            Assert.IsType<OkObjectResult>(result);
        }



        [Fact]
        public async Task Post_UsuarioInexistente_UnauthorizedResult()
        {
            //Arrange

            var input = new LoginModel { Email = "teste@gmail.com", Password = "teste123@" };
            var usuario = new Usuario { Nome = null, Email = null, Password = null, Profile = false };

            _usuarioControllerFixture.UsuarioRepositoryMock.Setup(r => r.ObterPorEmailAsync(input.Email)).ReturnsAsync(usuario);
            _usuarioControllerFixture.PasswordHasherMock.Setup(p => p.Verify(input.Password, usuario.Password));
            _usuarioControllerFixture.JwtServiceMock.Setup(j => j.GenerateToken(usuario)).Returns("token-falso");

            
            //Act
            var result = await _usuarioControllerFixture.Controller.Login(input);


            // Assert
            Assert.IsType<UnauthorizedResult>(result);
        }
    }
}
