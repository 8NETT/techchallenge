using FIAP.FCG.Core.Entity;
using FIAP.FCG.Core.Exceptions;
using FIAP.FCG.Core.Extensions;
using FIAP.FCG.Core.Validators;
using FluentValidation.Results;

namespace FIAP.FCG.Core.Builders
{
    public sealed class UsuarioBuilder
    {
        private Usuario _usuario = new Usuario();

        public UsuarioBuilder Id(int id) => this.Tee(b => b._usuario.Id = id);
        public UsuarioBuilder DataCriacao(DateTime data) => this.Tee(b => b._usuario.DataCriacao = data);
        public UsuarioBuilder Nome(string nome) => this.Tee(b => b._usuario.Nome = nome);
        public UsuarioBuilder Email(string email) => this.Tee(b => b._usuario.Email = email);
        public UsuarioBuilder Password(string password) => this.Tee(b => b._usuario.Password = password);
        public UsuarioBuilder Profile(bool profile) => this.Tee(b => b._usuario.Profile = profile);

        public ValidationResult Validate() =>
            new UsuarioValidator().Validate(_usuario);

        public Usuario Build()
        {
            if (!Validate().IsValid)
                throw new EstadoInvalidoException("Não é possível criar um usuário em um estado inválido.");

            return _usuario;
        }
    }
}
