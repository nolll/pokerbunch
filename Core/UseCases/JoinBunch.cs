using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Core.Entities;
using Core.Exceptions;
using Core.Repositories;
using Core.Services;
using ValidationException = Core.Exceptions.ValidationException;

namespace Core.UseCases
{
    public class JoinBunch
    {
        private readonly IBunchRepository _bunchRepository;

        public JoinBunch(IBunchRepository bunchRepository)
        {
            _bunchRepository = bunchRepository;
        }

        public Result Execute(Request request)
        {
            _bunchRepository.Join(request.BunchId, request.Code);
            return new Result(request.BunchId);
        }
        
        public class Request
        {
            public string BunchId { get; }
            public string Code { get; }

            public Request(string bunchId, string code)
            {
                BunchId = bunchId;
                Code = code;
            }
        }

        public class Result
        {
            public string BunchId { get; private set; }

            public Result(string bunchId)
            {
                BunchId = bunchId;
            }
        }
    }
}
