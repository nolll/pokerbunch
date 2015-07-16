using System.ComponentModel.DataAnnotations;

namespace Core.UseCases.JoinBunch
{
    public class JoinBunchRequest
    {
        public string Slug { get; private set; }
        public string UserName { get; private set; }
        [Required(ErrorMessage = "Code can't be empty")]
        public string Code { get; private set; }

        public JoinBunchRequest(string slug, string userName, string code)
        {
            Slug = slug;
            UserName = userName;
            Code = code;
        }
    }
}