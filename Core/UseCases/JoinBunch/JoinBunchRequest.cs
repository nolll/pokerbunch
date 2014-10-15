using System.ComponentModel.DataAnnotations;

namespace Core.UseCases.JoinBunch
{
    public class JoinBunchRequest
    {
        public string Slug { get; private set; }
        [Required(ErrorMessage = "Code can't be empty")]
        public string Code { get; private set; }

        public JoinBunchRequest(string slug, string code)
        {
            Slug = slug;
            Code = code;
        }
    }
}