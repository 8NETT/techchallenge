using Core.Repository;
using Moq;
using TechChallenge.Controllers;
using TechChallenge.Security;

namespace TechChallengeTest
{
    public class UsuarioControllerFixture
    {
        public Mock<IUsuarioRepository> UsuarioRepositoryMock{ get; }
        public Mock<IPasswordHasher> PasswordHasherMock{ get; }
        public Mock<IJwtService> JwtServiceMock{ get; }

        public UsuarioController Controller { get; }

        public UsuarioControllerFixture() {
        
            UsuarioRepositoryMock = new Mock<IUsuarioRepository>();
            PasswordHasherMock = new Mock<IPasswordHasher>();
            JwtServiceMock = new Mock<IJwtService>();

            Controller = new UsuarioController
                (
                UsuarioRepositoryMock.Object,
                PasswordHasherMock.Object,
                JwtServiceMock.Object
                );
        }
    }
}
