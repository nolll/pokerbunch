using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Tests.Common.Builders;

namespace Tests.Common
{
    public class MockContainer
    {
        private IDictionary<Type, Mock> _mocks;
        protected readonly CacheContainerInTest CacheContainer;

        public MockContainer()
        {
            CacheContainer = new CacheContainerInTest();
        }

        [SetUp]
        public void SetUpMocks()
        {
            _mocks = new Dictionary<Type, Mock>();
        }

        protected HomegameBuilder AHomegame
        {
            get { return new HomegameBuilder(); }
        }

        protected HomegameListBuilder AHomegameList
        {
            get { return new HomegameListBuilder(); }
        }

        protected UserBuilder AUser
        {
            get { return new UserBuilder(); }
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
