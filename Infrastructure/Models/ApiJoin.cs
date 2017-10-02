using JetBrains.Annotations;

namespace Infrastructure.Api.Models
{
    internal class ApiJoin
    {
        [UsedImplicitly]
        public string Code { get; set; }

        public ApiJoin(string code)
        {
            Code = code;
        }
    }
}