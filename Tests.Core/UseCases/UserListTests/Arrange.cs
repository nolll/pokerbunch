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

        private IList<User> TwoUsers => new List<User>
        {
            new User(UserData.Id1, UserData.UserName1, UserData.DisplayName1),
            new User(UserData.Id2, UserData.UserName2, UserData.DisplayName2),
        };
    }
}