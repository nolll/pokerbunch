using System.Collections.Generic;
using Core.Classes;
using Moq;
using NUnit.Framework;
using Web.ModelFactories.PageBaseModelFactories;

namespace Web.Tests.ModelFactories
{
    public class PagePropertiesFactoryTests
    {
        [Test]
        public void Create_WithoutHomeGame_HomegameNavModelIsNull()
        {
            var user = new User();

            var sut = GetSut();
            var result = sut.Create(user);

            Assert.IsNull(result.HomegameNavModel);
        }

        [Test]
        public void HomegameNavModel_WithOneHomeGame_IsNotNull()
        {
            var user = new User();
            var homegame = new Homegame();

            var sut = GetSut();
            var result = sut.Create(user, homegame);

            Assert.IsNotNull(result.HomegameNavModel);
        }

        private PagePropertiesFactory GetSut()
        {
            return new PagePropertiesFactory();
        }

    }
}
