using FIAP.FCG.Application.Security;
using FIAP.FCG.Application.Services;
using FIAP.FCG.Core.Repository;
using Moq;

namespace ApplicationTests.UsuarioServiceTests
{
    public class UsuarioServiceFixture
    {
        public Mock<IUnitOfWork> UnitOfWorkMock { get; } = new();
        public Mock<IPasswordHasher> PasswordHasherMock { get; } = new();
        public UsuarioService Service { get; }

        public UsuarioServiceFixture()
        {
            Service = new UsuarioService(UnitOfWorkMock.Object, PasswordHasherMock.Object);
        }
    }
}
