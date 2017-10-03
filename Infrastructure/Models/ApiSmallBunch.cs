using Core.Entities;
using JetBrains.Annotations;

namespace Infrastructure.Api.Models
{
    public class ApiSmallBunch
    {
        [UsedImplicitly]
        public string Id { get; set; }
        [UsedImplicitly]
        public string Name { get; set; }
        [UsedImplicitly]
        public string Description { get; set; }

        public ApiSmallBunch(Bunch b)
        {
            Id = b.Id;
            Name = b.DisplayName;
            Description = b.Description;
        }

        public ApiSmallBunch()
        {
        }
    }
}