using Core.Repository;
using Moq;
using TechChallenge.Controllers;
using TechChallenge.Security;

namespace TechChallengeTest.AccountControllerTests
{
    public class BaseTest
    {
        protected Mock<IUnitOfWork> UnitOfWork { get; } = new();
        protected Mock<IPasswordHasher> PasswordHasherMock { get; } = new();
        protected Mock<IJwtService> JwtServiceMock { get; } = new();
        protected AccountController Controller { get; }

        protected BaseTest()
        {
            Controller = new AccountController(
                UnitOfWork.Object,
                PasswordHasherMock.Object,
                JwtServiceMock.Object);
        }
    }
}
