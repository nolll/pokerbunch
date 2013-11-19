using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;

namespace Tests.Common
{
    public class MockContainer
    {
        private IDictionary<Type, Mock> _mocks;
        protected readonly CacheContainerFake CacheContainerFake;
        protected WebMocks Mocks;

        public MockContainer()
        {
            CacheContainerFake = new CacheContainerFake();
        }

        [SetUp]
        public void SetUpMocks()
        {
            _mocks = new Dictionary<Type, Mock>();
            Mocks = new WebMocks();
        }

        protected Mock<T> GetMock<T>() where T : class
        {
            Mock mock;
            var type = typeof(T);
            if (!_mocks.TryGetValue(type, out mock))
            {
                mock = new Mock<T>();
                _mocks.Add(type, mock);
            }
            return mock as Mock<T>;
        }
    }

}
