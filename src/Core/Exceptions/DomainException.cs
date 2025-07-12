namespace FIAP.FCG.Core.Exceptions
{
    public abstract class DomainException : Exception
    {
        public DomainException(string message) : base(message) { }
    }
}
