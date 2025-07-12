using FIAP.FCG.Core.Entity;
using FIAP.FCG.Core.Exceptions;

namespace CoreTests.Builders
{
    public class UsuarioBuilderTest
    {
        [Fact]
        public void Build_Success()
        {
            // Act
            var ex = Record.Exception(() => Usuario.New()
                .Id(1)
                .DataCriacao(DateTime.Now)
                .Nome("Teste")
                .Email("teste@example.com")
                .Password("hashedPassword")
                .Profile(true)
                .Build());

            // Assert
            Assert.Null(ex);
        }

        [Fact]
        public void Build_Failure_NomeVazio()
        {
            // Act
            var ex = Record.Exception(() => Usuario.New()
                .Id(1)
                .DataCriacao(DateTime.Now)
                .Email("teste@example.com")
                .Password("hashedPassword")
                .Profile(true)
                .Build());

            // Assert
            Assert.NotNull(ex);
            Assert.IsType<EstadoInvalidoException>(ex);
        }

        [Fact]
        public void Build_Failure_EmailVazio()
        {
            // Act
            var ex = Record.Exception(() => Usuario.New()
                .Id(1)
                .DataCriacao(DateTime.Now)
                .Nome("Teste")
                .Password("hashedPassword")
                .Profile(true)
                .Build());

            // Assert
            Assert.NotNull(ex);
            Assert.IsType<EstadoInvalidoException>(ex);
        }

        [Fact]
        public void Build_Failure_PasswordVazia()
        {
            // Act
            var ex = Record.Exception(() => Usuario.New()
                .Id(1)
                .DataCriacao(DateTime.Now)
                .Nome("Teste")
                .Email("teste@example.com")
                .Profile(true)
                .Build());

            // Assert
            Assert.NotNull(ex);
            Assert.IsType<EstadoInvalidoException>(ex);
        }

        [Fact]
        public void Build_Failure_DataCriacaoVazia()
        {
            // Act
            var ex = Record.Exception(() => Usuario.New()
                .Id(1)
                .Nome("Teste")
                .Email("teste@example.com")
                .Password("hashedPassword")
                .Profile(true)
                .Build());

            // Assert
            Assert.NotNull(ex);
            Assert.IsType<EstadoInvalidoException>(ex);
        }
    }
}
