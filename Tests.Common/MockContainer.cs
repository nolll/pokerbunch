using Infrastructure.Data.Storage.Interfaces;
using Infrastructure.System;
using Moq;
using NUnit.Framework;

namespace Tests.Common
{
    public class MockContainer
    {
        protected Mock<IHomegameStorage> _homegameStorageMock;
        protected Mock<IUserStorage> _userStorageMock;

        protected Mock<IWebContext> _webContextMock;

        [SetUp]
        public void SetUpMocks()
        {
            _homegameStorageMock = new Mock<IHomegameStorage>();
            _userStorageMock = new Mock<IUserStorage>();

            _webContextMock = new Mock<IWebContext>();
        }
    }
}
