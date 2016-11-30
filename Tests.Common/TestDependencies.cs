﻿using Tests.Common.FakeRepositories;
using Tests.Common.FakeServices;

namespace Tests.Common
{
    public class TestDependencies
    {
        public FakeBunchRepository Bunch { get; }
        public FakeUserRepository User { get; }
        public FakePlayerRepository Player { get; }
        public FakeCashgameRepository Cashgame { get; }
        public FakeEventRepository Event { get; }
        public FakeLocationRepository Location { get; }
        public FakeAppRepository App { get; }
        public FakeTokenRepository Token { get; }

        public FakeMessageSender MessageSender { get; }
        public FakeRandomService RandomService { get; }

        public TestDependencies()
        {
            Bunch = new FakeBunchRepository();
            User = new FakeUserRepository();
            Player = new FakePlayerRepository();
            Cashgame = new FakeCashgameRepository();
            Event = new FakeEventRepository();
            Location = new FakeLocationRepository();
            App = new FakeAppRepository();
            Token = new FakeTokenRepository();

            MessageSender = new FakeMessageSender();
            RandomService = new FakeRandomService();
        }
    }
}