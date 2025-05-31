using FIAP.FCG.Application.Services;
using FIAP.FCG.Core.Repository;
using Moq;

namespace ApplicationTests.JogoServiceTests
{
    public class JogoServiceFixture
    {
        public Mock<IUnitOfWork> UnitOfWorkMock { get; } = new();
        public JogoService Service { get; }

        public JogoServiceFixture()
        {
            Service = new JogoService(UnitOfWorkMock.Object);
        }
    }
}
