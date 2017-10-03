using JetBrains.Annotations;

namespace Infrastructure.Api.Models
{
    public class ApiEvent
    {
        [UsedImplicitly]
        public string Id { get; set; }
        [UsedImplicitly]
        public string BunchId { get; set; }
        [UsedImplicitly]
        public string Name { get; set; }
        [UsedImplicitly]
        public string StartDate { get; set; }
        [UsedImplicitly]
        public ApiEventLocation Location { get; set; }

        public ApiEvent(string name, string bunchId)
        {
            Name = name;
            BunchId = bunchId;
        }

        public ApiEvent()
        {
        }
    }
}