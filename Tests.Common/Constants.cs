using System;
using Tests.Common.Builders;

namespace Tests.Common
{
    public static class Constants
    {
        public const int BunchIdA = 1;
        public const int BunchIdB = 2;
        public const string SlugA = "bunch-a";
        public const string SlugB = "bunch-b";
        public const string BunchNameA = "Bunch A";
        public const string BunchNameB = "Bunch B";
        public const string DescriptionA = "Description A";
        public const string DescriptionB = "Description B";
        public const string HouseRulesA = "House Rules A";
        public const string HouseRulesB = "House Rules B";
        public const int DefaultBuyinA = 100;
        public const int DefaultBuyinB = 200;

        public const int UserIdA = 1;
        public const int UserIdB = 2;
        public const int UserIdC = 3;
        public const int UserIdD = 4;
        public const string UserNameA = "user-name-a";
        public const string UserNameB = "user-name-b";
        public const string UserNameC = "user-name-c";
        public const string UserNameD = "user-name-d";
        public const string UserEmailA = "email-a@example.com";
        public const string UserEmailB = "email-b@example.com";
        public const string UserEmailC = "email-c@example.com";
        public const string UserEmailD = "email-d@example.com";
        public const string UserRealNameA = "Real Name A";
        public const string UserRealNameB = "Real Name B";
        public const string UserRealNameC = "Real Name C";
        public const string UserRealNameD = "Real Name D";
        public const string UserDisplayNameA = "Display Name A";
        public const string UserDisplayNameB = "Display Name B";
        public const string UserDisplayNameC = "Display Name C";
        public const string UserDisplayNameD = "Display Name D";
        public const string UserPasswordA = "PasswordA";
        public const string UserPasswordB = "PasswordB";
        public const string UserEncryptedPasswordA = "5a99a164773c45966e5fcdd1c3110937861094aa";
        public const string UserEncryptedPasswordB = "6873088c1117d25d1abf4b75272d463b0ec6a504";
        public const string UserEncryptedPasswordC = "not_used_in_any_test_yet";
        public const string UserEncryptedPasswordD = "not_used_in_any_test_yet";
        public const string UserSaltA = "SaltA";
        public const string UserSaltB = "SaltB";
        public const string UserSaltC = "SaltC";
        public const string UserSaltD = "SaltD";

        public const int BuyinCheckpointId = 1;
        public const int BuyinCheckpointStack = 100;
        public const int BuyinCheckpointAmount = 200;
        public static DateTime BuyinCheckpointTimestamp = new DateTimeBuilder().AsUtc().Build();
        public const int ReportCheckpointId = 2; 
        public const int ReportCheckpointStack = 300;
        public const int ReportCheckpointAmount = 400;
        public static DateTime ReportCheckpointTimestamp = new DateTimeBuilder().AsUtc().Build();
        public const int CashoutCheckpointId = 3;
        public const int CashoutCheckpointStack = 500;
        public const int CashoutCheckpointAmount = 600;
        public static DateTime CashoutCheckpointTimestamp = new DateTimeBuilder().AsUtc().Build();

        public const int PlayerIdA = 1;
        public const int PlayerIdB = 2;
        public const int PlayerIdC = 3;
        public const int PlayerIdD = 4;
        public const string PlayerNameA = "Player Name A";
        public const string PlayerNameB = "Player Name B";
        public const string PlayerNameC = "Player Name C";
        public const string PlayerNameD = "Player Name D";

        public const int CashgameIdA = 1;
        public const int CashgameIdB = 2;
        public const int CashgameIdC = 3;
        public const string LocationA = "Location A";
        public const string LocationB = "Location B";
        public const string LocationC = "Location C";
        public const string DateStringA = "2001-01-01";
        public const string DateStringB = "2002-02-02";
        public const string DateStringC = "2003-03-03";
        public static DateTime StartTimeA = DateTime.Parse("2001-01-01 12:00:00");
        public static DateTime StartTimeB = DateTime.Parse("2002-02-02 12:00:00");
        public static DateTime StartTimeC = DateTime.Parse("2003-03-03 12:00:00");

        public const int EventIdA = 1;
        public const int EventIdB = 2;
        public const string EventNameA = "Event A";
        public const string EventNameB = "Event B";
    }
}