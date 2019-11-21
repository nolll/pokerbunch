using System.Linq;
using Core.Services;
using Microsoft.AspNetCore.Http;

namespace Web.Services
{
    public class TokenReader : ITokenReader
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TokenReader(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string Read()
        {
            return _httpContextAccessor.HttpContext?.User?.Claims.First(o => o.Type == "Token")?.Value;
        }
    }
}