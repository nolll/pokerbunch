using Core.Entities;

namespace Tests.Common.FakeClasses
{
    public class FakeTwitterCredentials : TwitterCredentials
    {
        public FakeTwitterCredentials(
            string key = default(string),
            string secret = default(string),
            string twitterName = default(string))
            : base(
                key, 
                secret, 
                twitterName)
        {
        }
    }
}