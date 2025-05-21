namespace TechChallenge.Security
{
    public class BCryptPasswordHasher : IPasswordHasher
    {
        private int _workFactor;

        public BCryptPasswordHasher(int workFactor)
        {
            _workFactor = workFactor;
        }

        public string Hash(string password) =>
            BCrypt.Net.BCrypt.HashPassword(password, _workFactor);

        public bool Verify(string password, string hash) =>
            BCrypt.Net.BCrypt.Verify(password, hash);
    }
}
