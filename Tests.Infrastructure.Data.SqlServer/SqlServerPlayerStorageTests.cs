using System.Collections.Generic;
using Infrastructure.Data.Factories.Interfaces;
using Infrastructure.Data.SqlServer;
using Moq;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Infrastructure.Data.SqlServer
{
    public class SqlServerPlayerStorageTests : MockContainer
    {
        [Test]
        public void JoinHomegame_CallsStorageWithCorrectSqlAndParameters()
        {
            const int playerId = 1;
            const int role = 2;
            const int homegameId = 3;
            const int userId = 4;

            const string expectedSql = "UPDATE player SET HomegameID = @homegameId, PlayerName = NULL, UserID = @userId, RoleID = @role, Approved = 1 WHERE PlayerID = @playerId";
            const string expectedPlayerIdParamName = "@playerId";
            const string expectedRoleParamName = "@role";
            const string expectedHomegameIdParamName = "@homegameId";
            const string expectedUserIdParamName = "@userId";
            string sentSql = null;
            IList<SimpleSqlParameter> sentParameters = null;

            GetMock<IStorageProvider>()
                .Setup(o => o.Execute(It.IsAny<string>(), It.IsAny<IList<SimpleSqlParameter>>()))
                .Callback<string, IList<SimpleSqlParameter>>((sql, parameters) => { sentSql = sql; sentParameters = parameters; });

            var sut = GetSut();
            sut.JoinHomegame(playerId, role, homegameId, userId);

            Assert.AreEqual(expectedSql, sentSql);
            Assert.AreEqual(expectedHomegameIdParamName, sentParameters[0].ParameterName);
            Assert.AreEqual(homegameId, sentParameters[0].Value);
            Assert.AreEqual(expectedUserIdParamName, sentParameters[1].ParameterName);
            Assert.AreEqual(userId, sentParameters[1].Value);
            Assert.AreEqual(expectedRoleParamName, sentParameters[2].ParameterName);
            Assert.AreEqual(role, sentParameters[2].Value);
            Assert.AreEqual(expectedPlayerIdParamName, sentParameters[3].ParameterName);
            Assert.AreEqual(playerId, sentParameters[3].Value);
            
        }

        [Test]
        public void DeletePlayer_CallsStorageWithCorrectSqlAndParameters()
        {
            var playerId = 1;

            const string expectedSql = "DELETE FROM player WHERE PlayerID = @playerId";
            const string expectedParamName = "@playerId";
            string sentSql = null;
            IList<SimpleSqlParameter> sentParameters = null;
            
            GetMock<IStorageProvider>()
                .Setup(o => o.Execute(It.IsAny<string>(), It.IsAny<IList<SimpleSqlParameter>>()))
                .Callback<string, IList<SimpleSqlParameter>>((sql, parameters) => { sentSql = sql; sentParameters = parameters; });

            var sut = GetSut();
            sut.DeletePlayer(playerId);

            Assert.AreEqual(expectedSql, sentSql);
            Assert.AreEqual(expectedParamName, sentParameters[0].ParameterName);
            Assert.AreEqual(playerId, sentParameters[0].Value);
        }

        private SqlServerPlayerStorage GetSut()
        {
            return new SqlServerPlayerStorage(
                GetMock<IStorageProvider>().Object,
                GetMock<IRawPlayerFactory>().Object);
        }
    }
}
