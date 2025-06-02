using FIAP.FCG.Core.Entity;
using FluentValidation;

namespace FIAP.FCG.Core.Validators
{
    internal class CompraValidator : AbstractValidator<Compra>
    {
        public CompraValidator()
        {
            RuleFor(c => c.DataCriacao)
                .NotEmpty();

            RuleFor(c => c.JogoId)
                .NotEmpty().WithMessage("O ID do jogo deve ser preenchido.");

            RuleFor(c => c.Comprador)
                .NotEmpty().WithMessage("O ID comprador deve ser preenchido.");

            RuleFor(c => c.Valor)
                .NotEmpty().WithMessage("O valor deve ser preenchido.")
                .GreaterThanOrEqualTo(0M).WithMessage("O valor não pode ser negativo.");
        }
    }
}
