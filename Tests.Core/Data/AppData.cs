using System.Collections.Generic;
using Core.Entities;

namespace Tests.Core.Data
{
    public static class AppData
    {
        public const string Id1 = "app-id-1";
        public const string Key1 = "app-key-1";
        public const string Name1 = "app-name-1";

        public const string Id2 = "app-id-2";
        public const string Key2 = "app-key-2";
        public const string Name2 = "app-name-2";

        public static App OneApp => new App(Id1, Key1, Name1, UserData.Id1);

        public static IList<App> TwoApps => new List<App>
        {
            OneApp,
            new App(Id2, Key2, Name2, UserData.Id2)
        };
    }
}