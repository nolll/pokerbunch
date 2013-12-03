﻿using System.Collections.Generic;
using Core.Classes;
using Core.Exceptions;
using Core.Repositories;
using Core.Services;
using Moq;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;
using Web.ModelFactories.CashgameModelFactories.Add;
using Web.ModelFactories.CashgameModelFactories.Details;
using Web.ModelFactories.CashgameModelFactories.Edit;
using Web.ModelFactories.CashgameModelFactories.Facts;
using Web.ModelFactories.CashgameModelFactories.Leaderboard;
using Web.ModelFactories.CashgameModelFactories.Matrix;
using Web.ModelServices;
using Web.Models.CashgameModels.Details;
using Web.Models.CashgameModels.Leaderboard;
using Web.Models.CashgameModels.Matrix;

namespace Web.Tests.ModelServiceTests
{
    public class CashgameModelServiceTests : MockContainer
    {
        [Test]
        public void GetMatrixModel_Authorized_ReturnsModel()
        {
            const string slug = "a";
            GetMock<IUserContext>().Setup(o => o.GetUser()).Returns(new FakeUser());
            GetMock<IMatrixPageModelFactory>().Setup(o => o.Create(It.IsAny<Homegame>(), It.IsAny<User>(), It.IsAny<int?>())).Returns(new CashgameMatrixPageModel());

            var sut = GetSut();
            var result = sut.GetMatrixModel(slug);

            Assert.IsNotNull(result);
        }

        [Test]
        public void GetMatrixModel_NotAuthorized_ThrowsException()
        {
            const string slug = "a";
            GetMock<IUserContext>().Setup(o => o.RequirePlayer(It.IsAny<Homegame>())).Throws<AccessDeniedException>();

            var sut = GetSut();

            Assert.Throws<AccessDeniedException>(() => sut.GetMatrixModel(slug));
        }

        [Test]
        public void GetLeaderboardModel_Authorized_ReturnsModel()
        {
            const string slug = "a";
            GetMock<IUserContext>().Setup(o => o.GetUser()).Returns(new FakeUser());
            GetMock<ICashgameLeaderboardPageModelFactory>().Setup(o => o.Create(It.IsAny<User>(), It.IsAny<Homegame>(), It.IsAny<CashgameSuite>(), It.IsAny<IList<int>>(), LeaderboardSortOrder.winnings, It.IsAny<int?>())).Returns(new CashgameLeaderboardPageModel());

            var sut = GetSut();
            var result = sut.GetLeaderboardModel(slug, LeaderboardSortOrder.winnings);

            Assert.IsNotNull(result);
        }

        [Test]
        public void GetLeaderboardModel_NotAuthorized_ThrowsException()
        {
            const string slug = "a";
            GetMock<IUserContext>().Setup(o => o.RequirePlayer(It.IsAny<Homegame>())).Throws<AccessDeniedException>();

            var sut = GetSut();

            Assert.Throws<AccessDeniedException>(() => sut.GetLeaderboardModel(slug, LeaderboardSortOrder.winnings));
        }

        [Test]
        public void GetDetailsModel_NotAuthorized_ThrowsException()
        {
            const string slug = "a";
            const string dateStr = "2000-01-01";
            GetMock<IUserContext>().Setup(o => o.RequirePlayer(It.IsAny<Homegame>())).Throws<AccessDeniedException>();

            var sut = GetSut();

            Assert.Throws<AccessDeniedException>(() => sut.GetDetailsModel(slug, dateStr));
        }

        [Test]
        public void GetDetailsModel_ReturnsModel()
        {
            const string slug = "a";
            const string dateStr = "2000-01-01"; 
            GetMock<IUserContext>().Setup(o => o.GetUser()).Returns(new FakeUser());
            GetMock<ICashgameRepository>().Setup(o => o.GetByDateString(It.IsAny<Homegame>(), It.IsAny<string>())).Returns(new FakeCashgame());
            GetMock<ICashgameDetailsPageModelFactory>().Setup(o => o.Create(It.IsAny<User>(), It.IsAny<Homegame>(), It.IsAny<Cashgame>(), It.IsAny<Player>(), It.IsAny<bool>())).Returns(new CashgameDetailsPageModel());

            var sut = GetSut();
            var result = sut.GetDetailsModel(slug, dateStr);

            Assert.IsNotNull(result);
        }

        private CashgameModelService GetSut()
        {
            return new CashgameModelService(
                GetMock<IHomegameRepository>().Object,
                GetMock<IUserContext>().Object,
                GetMock<IMatrixPageModelFactory>().Object,
                GetMock<ICashgameService>().Object,
                GetMock<ICashgameRepository>().Object,
                GetMock<ICashgameLeaderboardPageModelFactory>().Object,
                GetMock<IUrlProvider>().Object,
                GetMock<ICashgameDetailsPageModelFactory>().Object,
                GetMock<IPlayerRepository>().Object,
                GetMock<ICashgameDetailsChartModelFactory>().Object,
                GetMock<ICashgameFactsPageModelFactory>().Object,
                GetMock<IAddCashgamePageModelFactory>().Object,
                GetMock<ICashgameEditPageModelFactory>().Object);
        }

    }
}
