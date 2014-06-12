using Core.Entities;

namespace Tests.Common.FakeClasses
{
    public class TwitterCredentialsInTest : TwitterCredentials
    {
        public TwitterCredentialsInTest(
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