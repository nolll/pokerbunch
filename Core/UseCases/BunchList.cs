﻿using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Repositories;
using Core.Services;

namespace Core.UseCases
{
    public class BunchList
    {
        private readonly BunchService _bunchService;
        private readonly UserService _userService;

        public BunchList(BunchService bunchService, UserService userService)
        {
            _bunchService = bunchService;
            _userService = userService;
        }

        public Result Execute(AllBunchesRequest request)
        {
            var user = _userService.GetByNameOrEmail(request.UserName);
            RoleHandler.RequireAdmin(user);

            var bunches = _bunchService.GetList();
            return new Result(bunches);
        }

        public Result Execute(UserBunchesRequest request)
        {
            var user = _userService.GetByNameOrEmail(request.UserName);
            var homegames = user != null ? _bunchService.GetByUserId(user.Id) : new List<Bunch>();
            
            return new Result(homegames);
        }

        public class AllBunchesRequest
        {
            public string UserName { get; private set; }

            public AllBunchesRequest(string userName)
            {
                UserName = userName;
            }
        }

        public class UserBunchesRequest
        {
            public string UserName { get; private set; }

            public UserBunchesRequest(string userName)
            {
                UserName = userName;
            }
        }

        public class Result
        {
            public IList<ResultItem> Bunches { get; private set; }

            public Result(IEnumerable<Bunch> homegames)
            {
                Bunches = homegames.Select(o => new ResultItem(o)).ToList();
            }
        }

        public class ResultItem
        {
            public string Slug { get; private set; }
            public string DisplayName { get; private set; }

            public ResultItem(Bunch bunch)
            {
                Slug = bunch.Slug;
                DisplayName = bunch.DisplayName;
            }
        }
    }
}