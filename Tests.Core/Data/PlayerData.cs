using System.Collections.Generic;
using Core.Entities;

namespace Tests.Core.Data
{
    public static class PlayerData
    {
        public const string Id1 = "player-id-1";
        public const string Name1 = "player-name-1";
        public const string DisplayName1 = "player-display-name-1";
        public const string Color1 = "#111";

        public const string Id2 = "player-id-2";
        public const string Name2 = "player-name-2";
        public const string DisplayName2 = "player-display-name-1";
        public const string Color2 = "#222";

        public static IList<Player> TwoPlayers = new List<Player>
        {
            new Player(BunchData.Id1, Id1, null, Name1, Role.Player, Color1),
            new Player(BunchData.Id1, Id2, null, Name2, Role.Player, Color2)
        };
    }
}