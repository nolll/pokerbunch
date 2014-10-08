using Core.Entities;
using Tests.Common.FakeClasses;

namespace Tests.Common.Builders
{
    public class CashgameTotalResultBuilder
    {
        private int _winnings;
        private int _buyin;
        private int _cashout;
        private int _minutesPlayed;
        private int _gameCount;
        private int _winRate;
        private Player _player;

        public CashgameTotalResultBuilder()
        {
            _player = new PlayerBuilder().Build();
        }

        public CashgameTotalResult Build()
        {
            return new CashgameTotalResultInTest(_winnings, _gameCount, _minutesPlayed, _winRate, _player, _buyin, _cashout);
        }

        public CashgameTotalResultBuilder WithWinnings(int winnings)
        {
            _winnings = winnings;
            return this;
        }

        public CashgameTotalResultBuilder WithBuyin(int buyin)
        {
            _buyin = buyin;
            return this;
        }

        public CashgameTotalResultBuilder WithCashout(int cashout)
        {
            _cashout = cashout;
            return this;
        }

        public CashgameTotalResultBuilder WithMinutesPlayed(int minutes)
        {
            _minutesPlayed = minutes;
            return this;
        }

        public CashgameTotalResultBuilder WithGamesPlayed(int gameCount)
        {
            _gameCount = gameCount;
            return this;
        }

        public CashgameTotalResultBuilder WithWinRate(int winRate)
        {
            _winRate = winRate;
            return this;
        }

        public CashgameTotalResultBuilder WithPlayer(Player player)
        {
            _player = player;
            return this;
        }
    }
}