using FIAP.FCG.Application.Contracts;
using FIAP.FCG.WebApi.Controllers;
using Moq;

namespace WebApiTests.UsuarioControllerTests
{
    public class UsuarioControllerFixture
    {
        public Mock<IUsuarioService> UsuarioServiceMock { get; } = new();
        public UsuarioController Controller { get; }

        public UsuarioControllerFixture()
        {
            Controller = new UsuarioController(UsuarioServiceMock.Object);
        }
    }
}
