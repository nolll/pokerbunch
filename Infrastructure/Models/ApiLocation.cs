using JetBrains.Annotations;

namespace Infrastructure.Api.Models
{
    public class ApiLocation
    {
        [UsedImplicitly]
        public string Id { get; set; }
        [UsedImplicitly]
        public string Name { get; set; }
        [UsedImplicitly]
        public string Bunch { get; set; }

        public ApiLocation(string name, string bunch)
        {
            Name = name;
            Bunch = bunch;
        }

        public ApiLocation()
        {
        }
    }
}