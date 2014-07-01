namespace Application.UseCases.AddCashgame
{
    public class AddCashgameRequest
    {
        public string Slug { get; set; }
        public string Location { get; set; }

        public AddCashgameRequest(string slug, string location)
        {
            Slug = slug;
            Location = location;
        }

        public bool HasLocation
        {
            get { return !string.IsNullOrEmpty(Location); }
        }
    }
}