using System.ComponentModel.DataAnnotations;

namespace Application.UseCases.InvitePlayer
{
    public class InvitePlayerRequest
    {
        public string Slug { get; private set; }
        public int PlayerId { get; private set; }
        [Required(ErrorMessage = "Email can't be empty")]
        [EmailAddress(ErrorMessage = "The email address is not valid")]
        public string Email { get; private set; }

        public InvitePlayerRequest(string slug, int playerId, string email)
        {
            Slug = slug;
            PlayerId = playerId;
            Email = email;
        }
    }
}