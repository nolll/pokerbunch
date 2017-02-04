using Core.Entities;

namespace Tests.Core.Data
{
    public static class BunchData
    {
        public const string Id1 = "bunch-id-1";
        public const string DisplayName1 = "bunch-displayname-1";
        public const string Description1 = "bunch-description-1";
        public const string HouseRules1 = "bunch-houserules-1";

        public static Bunch Bunch(Role role)
        {
            return new Bunch(Id1, DisplayName1, Description1, HouseRules1, null, 0, null, role);
        }
    }
}