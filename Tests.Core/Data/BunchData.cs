using Core.Entities;

namespace Tests.Core.Data
{
    public static class BunchData
    {
        public const string Id1 = "bunch-id-1";
        public const string DisplayName1 = "bunch-displayname-1";
        public const string Description1 = "bunch-description-1";
        public const string HouseRules1 = "bunch-houserules-1";

        public const string Id2 = "bunch-id-2";
        public const string DisplayName2 = "bunch-displayname-2";
        public const string Description2 = "bunch-description-2";
        public const string HouseRules2 = "bunch-houserules-2";

        public static Bunch Bunch1(Role role)
        {
            return new Bunch(Id1, DisplayName1, Description1, HouseRules1, null, 0, null, role);
        }

        public static Bunch Bunch2(Role role)
        {
            return new Bunch(Id2, DisplayName2, Description2, HouseRules2, null, 0, null, role);
        }
    }
}