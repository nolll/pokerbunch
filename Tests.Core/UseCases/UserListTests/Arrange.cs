using System.Collections.Generic;
using Core.Entities;
using Core.Services;
using Core.UseCases;
using Tests.Core.Data;

namespace Tests.Core.UseCases.UserListTests
{
    public abstract class Arrange : UseCaseTest<UserList>
    {
        protected UserList.Result Result;

        protected override void Setup()
        {
            Mock<IUserService>().Setup(o => o.List()).Returns(TwoUsers);
        }

        protected override void Execute()
        {
            Result = Subject.Execute();
        }

        private IList<ListUser> TwoUsers => new List<ListUser>
        {
            new ListUser(UserData.UserName1, UserData.DisplayName1),
            new ListUser(UserData.UserName2, UserData.DisplayName2),
        };
    }
}