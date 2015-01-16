﻿using System;
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
        public const string UserNameA = "user-name-a";
        public const string UserNameB = "user-name-b";
        public const string UserEmailA = "email-a@example.com";
        public const string UserEmailB = "email-b@example.com";
        public const string UserRealNameA = "Real Name A";
        public const string UserRealNameB = "Real Name B";
        public const string UserDisplayNameA = "Display Name A";
        public const string UserDisplayNameB = "Display Name B";
        public const string UserPasswordA = "PasswordA";
        public const string UserPasswordB = "PasswordB";
        public const string UserEncryptedPasswordA = "5a99a164773c45966e5fcdd1c3110937861094aa";
        public const string UserEncryptedPasswordB = "6873088c1117d25d1abf4b75272d463b0ec6a504";
        public const string UserSaltA = "SaltA";
        public const string UserSaltB = "SaltB";

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
        public const string PlayerNameA = "Player Name A";
        public const string PlayerNameB = "Player Name B";
        public const string PlayerNameC = "Player Name C";

        public const int CashgameIdA = 1;
        public const int CashgameIdB = 2;
        public const string LocationA = "Location A";
        public const string LocationB = "Location B";
        public const string DateStringA = "2001-02-03";
        public const string DateStringB = "2002-03-04";
        public static DateTime StartTimeA = DateTime.Parse("2001-02-03 04:05:06");
        public static DateTime StartTimeB = DateTime.Parse("2002-03-04 05:06:07");

        public const int EventIdA = 1;
        public const int EventIdB = 2;
        public const string EventNameA = "Event A";
        public const string EventNameB = "Event B";
    }
}