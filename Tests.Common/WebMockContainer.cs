using NUnit.Framework;

namespace Tests.Common
{
    public class WebMockContainer
    {
        protected WebMocks Mocks;

        [SetUp]
        public void SetUpMocks()
        {
            Mocks = new WebMocks();
        }

    }

}
