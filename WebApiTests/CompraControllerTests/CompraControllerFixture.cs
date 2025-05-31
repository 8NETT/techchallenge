using FIAP.FCG.Application.Contracts;
using FIAP.FCG.WebApi.Controllers;
using Moq;

namespace WebApiTests.CompraControllerTests
{
    public class CompraControllerFixture
    {
        public Mock<ICompraService> CompraServiceMock { get; } = new();
        public CompraController Controller { get; }

        public CompraControllerFixture()
        {
            Controller = new CompraController(CompraServiceMock.Object);
        }
    }
}
