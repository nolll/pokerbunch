namespace Core.UseCases.RunningCashgame
{
    public class RunningCashgameRequest
    {
        public string Slug { get; private set; }

        public RunningCashgameRequest(string slug)
        {
            Slug = slug;
        }
    }
}