namespace Core.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        IUsuarioRepository UsuarioRepository { get; }
        IJogoRepository JogoRepository { get; }
        Task CommitAsync();
    }
}
