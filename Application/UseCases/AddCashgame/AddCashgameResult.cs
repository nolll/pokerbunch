using System.Collections.Generic;

namespace Application.UseCases.AddCashgame
{
    public class AddCashgameResult
    {
        public bool CreatedGame { get; private set; }
        public IEnumerable<string> Errors { get; private set; }

        public AddCashgameResult(Validator validator)
        {
            CreatedGame = validator.IsValid;
            Errors = validator.Errors;
        }
    }
}