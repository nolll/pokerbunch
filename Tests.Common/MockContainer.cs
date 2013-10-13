using NUnit.Framework;

namespace Tests.Common
{
    public class MockContainer
    {
        protected WebMocks WebMocks;

        [SetUp]
        public void SetUpMocks()
        {
            WebMocks = new WebMocks();
        }

    }

}
