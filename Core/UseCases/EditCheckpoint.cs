using System;
using System.ComponentModel.DataAnnotations;
using Core.Entities.Checkpoints;
using Core.Repositories;
using Core.Services;
using ValidationException = Core.Exceptions.ValidationException;

namespace Core.UseCases
{
    public class EditCheckpoint
    {
        private readonly IBunchRepository _bunchRepository;
        private readonly ICashgameRepository _cashgameRepository;

        public EditCheckpoint(IBunchRepository bunchRepository, ICashgameRepository cashgameRepository)
        {
            _bunchRepository = bunchRepository;
            _cashgameRepository = cashgameRepository;
        }

        public Result Execute(Request request)
        {
            var validator = new Validator(request);
            if(!validator.IsValid)
                throw new ValidationException(validator);

            var cashgame = _cashgameRepository.GetDetailedById(request.CashgameId);
            var existingCheckpoint = cashgame.GetAction(request.ActionId);
            var bunch = _bunchRepository.Get(cashgame.BunchId);
            
            _cashgameRepository.UpdateAction(existingCheckpoint.Id, request.Timestamp, request.Stack, request.Amount);

            return new Result(cashgame.Id, existingCheckpoint.PlayerId);
        }

        public class Request
        {
            public string CashgameId { get; }
            public string ActionId { get; }
            public DateTime Timestamp { get; }
            public int Stack { get; }
            public int Amount { get; }

            public Request(string cashgameId, string actionId, DateTime timestamp, int stack, int amount)
            {
                CashgameId = cashgameId;
                ActionId = actionId;
                Timestamp = timestamp;
                Stack = stack;
                Amount = amount;
            }
        }

        public class Result
        {
            public string CashgameId { get; private set; }
            public string PlayerId { get; private set; }

            public Result(string cashgameId, string playerId)
            {
                CashgameId = cashgameId;
                PlayerId = playerId;
            }
        }
    }
}
