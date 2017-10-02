namespace Infrastructure.Api.Models
{
    internal class ApiAddCashgame
    {
        public string LocationId { get; }

        public ApiAddCashgame(string locationId)
        {
            LocationId = locationId;
        }
    }
}