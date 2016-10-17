﻿using Core.Services;
using Tests.Common.FakeServices;

namespace Tests.Common
{
    public class ServiceContainer
    {
        public FakeMessageSender MessageSender { get; }
        public FakeRandomService RandomService { get; }
        public CashgameService CashgameService { get; }
        public UserService UserService { get; }
        public EventService EventService { get; }
        public PlayerService PlayerService { get; }
        public AppService AppService { get; }
        public AuthService AuthService { get; }

        public ServiceContainer(RepositoryContainer repos)
        {
            MessageSender = new FakeMessageSender();
            RandomService = new FakeRandomService();
            CashgameService = new CashgameService(repos.Cashgame);
            UserService = new UserService(repos.User);
            EventService = new EventService(repos.Event);
            PlayerService = new PlayerService(repos.Player);
            AppService = new AppService(repos.App);
            AuthService = new AuthService(repos.Token);
        }

        public void Clear()
        {
            MessageSender.Reset();
        }
    }
}