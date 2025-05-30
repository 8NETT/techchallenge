using Core.Repository;
using Moq;
using TechChallenge.Controllers;
using TechChallenge.Security;

namespace TechChallengeTest.UsuarioControllerTests
{
    public class BaseTest
    {
        protected Mock<IUnitOfWork> UnitOfWorkMock { get; } = new();
        protected Mock<IPasswordHasher> PasswordHasherMock { get; } = new();
        protected UsuarioController Controller { get; }

        protected BaseTest()
        {
            Controller = new UsuarioController(
                UnitOfWorkMock.Object,
                PasswordHasherMock.Object);
        }
    }
}
