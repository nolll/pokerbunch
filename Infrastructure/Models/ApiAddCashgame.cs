namespace Infrastructure.Api.Models
{
    public class ApiAddCashgame
    {
        public string BunchId { get; }
        public string LocationId { get; }

        public ApiAddCashgame(string bunchId, string locationId)
        {
            BunchId = bunchId;
            LocationId = locationId;
        }
    }
}