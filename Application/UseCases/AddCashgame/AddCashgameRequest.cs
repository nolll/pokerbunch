using System.ComponentModel.DataAnnotations;

namespace Application.UseCases.AddCashgame
{
    public class AddCashgameRequest
    {
        public string Slug { get; private set; }
        [Required(ErrorMessage = "Please select or enter a location")]
        public string Location { get; private set; }

        public AddCashgameRequest(string slug, string location)
        {
            Slug = slug;
            Location = location;
        }
    }
}