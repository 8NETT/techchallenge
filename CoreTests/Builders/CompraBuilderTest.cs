using FIAP.FCG.Core.Entity;
using FIAP.FCG.Core.Exceptions;

namespace CoreTests.Builders
{
    public class CompraBuilderTest
    {
        [Fact]
        public void Build_Success()
        {
            // Act
            var ex = Record.Exception(() => Compra.New()
                .Id(1)
                .DataCriacao(DateTime.Now)
                .CompradorId(1)
                .JogoId(1)
                .Valor(100M)
                .Desconto(0)
                .Total(100M)
                .Build());

            // Assert
            Assert.Null(ex);
        }

        [Fact]
        public void Build_Failure_CompradorVazio()
        {
            // Act
            var ex = Record.Exception(() => Compra.New()
                .Id(1)
                .DataCriacao(DateTime.Now)
                .JogoId(1)
                .Valor(100M)
                .Desconto(0)
                .Total(100M)
                .Build());

            // Assert
            Assert.NotNull(ex);
            Assert.IsType<EstadoInvalidoException>(ex);
        }

        [Fact]
        public void Build_Failure_JogoVazio()
        {
            // Act
            var ex = Record.Exception(() => Compra.New()
                .Id(1)
                .DataCriacao(DateTime.Now)
                .CompradorId(1)
                .Valor(100M)
                .Desconto(0)
                .Total(100M)
                .Build());

            // Assert
            Assert.NotNull(ex);
            Assert.IsType<EstadoInvalidoException>(ex);
        }

        [Fact]
        public void Build_Failure_ValorNegativo()
        {
            // Act
            var ex = Record.Exception(() => Compra.New()
                .Id(1)
                .DataCriacao(DateTime.Now)
                .CompradorId(1)
                .JogoId(1)
                .Valor(-100M)
                .Desconto(0)
                .Total(100M)
                .Build());

            // Assert
            Assert.NotNull(ex);
            Assert.IsType<EstadoInvalidoException>(ex);
        }

        [Fact]
        public void Build_Failure_DescontoInvalido()
        {
            // Act
            var ex = Record.Exception(() => Compra.New()
                .Id(1)
                .DataCriacao(DateTime.Now)
                .CompradorId(1)
                .JogoId(1)
                .Valor(100M)
                .Desconto(-10)
                .Total(100M)
                .Build());

            // Assert
            Assert.NotNull(ex);
            Assert.IsType<EstadoInvalidoException>(ex);
        }

        [Fact]
        public void Build_Failure_TotalNegativo()
        {
            // Act
            var ex = Record.Exception(() => Compra.New()
                .Id(1)
                .DataCriacao(DateTime.Now)
                .JogoId(1)
                .Valor(100M)
                .Desconto(0)
                .Total(-100M)
                .Build());

            // Assert
            Assert.NotNull(ex);
            Assert.IsType<EstadoInvalidoException>(ex);
        }
    }
}
