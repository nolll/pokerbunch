using JetBrains.Annotations;

namespace Infrastructure.Api.Models
{
    public class ApiJoin
    {
        [UsedImplicitly]
        public string BunchId { get; set; }

        [UsedImplicitly]
        public string Code { get; set; }

        public ApiJoin(string bunchId, string code)
        {
            BunchId = bunchId;
            Code = code;
        }
    }
}