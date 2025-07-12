using FIAP.FCG.Core.Entity;
using FIAP.FCG.Core.Exceptions;

namespace CoreTests.Builders
{
    public class JogoBuilderTest
    {
        [Fact]
        public void Build_Success()
        {
            // Act
            var ex = Record.Exception(() => Jogo.New()
                .Id(1)
                .DataCriacao(DateTime.Now)
                .Nome("Teste")
                .Valor(100M)
                .Desconto(0)
                .Build());

            // Assert
            Assert.Null(ex);
        }

        [Fact]
        public void Build_Failure_NomeVazio()
        {
            // Act
            var ex = Record.Exception(() => Jogo.New()
                .Id(1)
                .DataCriacao(DateTime.Now)
                .Valor(100M)
                .Desconto(0)
                .Build());

            // Assert
            Assert.NotNull(ex);
            Assert.IsType<EstadoInvalidoException>(ex);
        }

        [Fact]
        public void Build_Failure_ValorNegativo()
        {
            // Act
            var ex = Record.Exception(() => Jogo.New()
                .Id(1)
                .DataCriacao(DateTime.Now)
                .Nome("Teste")
                .Valor(-100M)
                .Desconto(0)
                .Build());

            // Assert
            Assert.NotNull(ex);
            Assert.IsType<EstadoInvalidoException>(ex);
        }

        [Fact]
        public void Build_Failure_DescontoInvalido()
        {
            // Act
            var ex = Record.Exception(() => Jogo.New()
                .Id(1)
                .DataCriacao(DateTime.Now)
                .Nome("Teste")
                .Valor(100M)
                .Desconto(-10)
                .Build());

            // Assert
            Assert.NotNull(ex);
            Assert.IsType<EstadoInvalidoException>(ex);
        }
    }
}
