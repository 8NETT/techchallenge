using FIAP.FCG.Application.Contracts;
using FIAP.FCG.WebApi.Controllers;
using Moq;

namespace WebApiTests.JogoControllerTests
{
    public class JogoControllerFixture
    {
        public Mock<IJogoService> JogoServiceMock { get; } = new();
        public JogoController Controller { get; }

        public JogoControllerFixture()
        {
            Controller = new JogoController(JogoServiceMock.Object);
        }
    }
}
