namespace Core.UseCases.EndCashgame
{
    public class EndCashgameRequest
    {
        public string Slug { get; private set; }

        public EndCashgameRequest(string slug)
        {
            Slug = slug;
        }
    }
}