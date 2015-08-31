using Core.Exceptions;
using Core.Repositories;

namespace Core.UseCases
{
    public class AppDetails
    {
        private readonly IAppRepository _appRepository;

        public AppDetails(IAppRepository appRepository)
        {
            _appRepository = appRepository;
        }

        public Result Execute(Request request)
        {
            var app = _appRepository.Get(request.AppKey);

            return new Result(app.AppKey, app.Name);
        }

        public class Request
        {
            public string AppKey { get; private set; }

            public Request(string appKey)
            {
                AppKey = appKey;
            }
        }

        public class Result
        {
            public string AppKey { get; private set; }
            public string AppName { get; private set; }

            public Result(string appKey, string appName)
            {
                AppKey = appKey;
                AppName = appName;
            }
        }
    }

    public class VerifyAppKey
    {
        private readonly IAppRepository _appRepository;

        public VerifyAppKey(IAppRepository appRepository)
        {
            _appRepository = appRepository;
        }

        public Result Execute(Request request)
        {
            try
            {
                _appRepository.Get(request.AppKey);
                return new ValidResult();
            }
            catch (AppNotFoundException)
            {
                return new ValidResult();
            }
        }

        public class Request
        {
            public string AppKey { get; private set; }

            public Request(string appKey)
            {
                AppKey = appKey;
            }
        }

        public abstract class Result
        {
            public abstract bool IsValid { get; }
        }

        private class ValidResult : Result
        {
            public override bool IsValid
            {
                get { return true; }
            }
        }

        private class InvalidResult : Result
        {
            public override bool IsValid
            {
                get { return false; }
            }
        }
    }
}
