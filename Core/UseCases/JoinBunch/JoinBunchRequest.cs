using System.ComponentModel.DataAnnotations;

namespace Core.UseCases.JoinBunch
{
    public class JoinBunchRequest
    {
        public string Slug { get; private set; }
        public int UserId { get; private set; }
        [Required(ErrorMessage = "Code can't be empty")]
        public string Code { get; private set; }

        public JoinBunchRequest(string slug, int userId, string code)
        {
            Slug = slug;
            UserId = userId;
            Code = code;
        }
    }
}