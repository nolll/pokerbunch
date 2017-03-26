﻿using Core.Entities;
using Core.Exceptions;
using Core.Services;

namespace Core.UseCases
{
    public class BunchContext
    {
        private readonly IBunchService _bunchService;

        public BunchContext(IBunchService bunchService)
        {
            _bunchService = bunchService;
        }

        public Result Execute(CoreContext.Result coreContext, Request request)
        {
            var bunch = GetBunch(coreContext, request);
            return GetResult(coreContext, bunch);
        }

        private Result GetResult(CoreContext.Result appContext, SmallBunch bunch)
        {
            if (bunch == null)
                return new Result(appContext);

            return new Result(appContext, bunch.Id, bunch.Id, bunch.DisplayName);
        }

        private SmallBunch GetBunch(CoreContext.Result appContext, Request request)
        {
            if (!appContext.IsLoggedIn)
                return null;

            if (!string.IsNullOrEmpty(request.BunchId))
            {
                try
                {
                    return _bunchService.Get(request.BunchId);
                }
                catch (BunchNotFoundException)
                {
                    return null;
                }
            }
            var bunches = _bunchService.ListForUser();
            return bunches.Count == 1 ? bunches[0] : null;
        }

        public class Request
        {
            public string BunchId { get; }

            public Request(string bunchId = null)
            {
                BunchId = bunchId;
            }
        }

        public class Result
        {
            public string BunchId { get; private set; }
            public string Slug { get; private set; }
            public string BunchName { get; private set; }
            public bool HasBunch { get; private set; }
            public CoreContext.Result AppContext { get; private set; }

            public Result(CoreContext.Result appContextResult)
            {
                AppContext = appContextResult;
            }

            public Result(CoreContext.Result appContextResult, string slug, string bunchId, string bunchName)
                : this(appContextResult)
            {
                BunchId = bunchId;
                Slug = slug;
                BunchName = bunchName;
                HasBunch = true;
            }
        }
    }
}