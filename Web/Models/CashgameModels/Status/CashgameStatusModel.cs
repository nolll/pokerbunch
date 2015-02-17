using Core.UseCases.CashgameStatus;

namespace Web.Models.CashgameModels.Status
{
    public class CashgameStatusModel
    {
        public string Heading { get; private set; }
        public string LinkText { get; private set; }
        public string Url { get; private set; }

        public CashgameStatusModel(CashgameStatusResult statusResult)
        {
            Heading = statusResult.GameIsRunning ? "Running Game" : "New Game";
            LinkText = statusResult.GameIsRunning ? "Go to game" : "Start game";
            Url = statusResult.GameIsRunning ? statusResult.RunningCashgameUrl.Relative : statusResult.AddCashgameUrl.Relative;
        }
    }
}