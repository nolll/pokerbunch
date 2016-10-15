using Core.Repositories;

namespace Tests.Common.FakeRepositories
{
    public class FakeTokenRepository : ITokenRepository
    {
        public string Get(string userName, string password)
        {
            return "";
        }
    }
}