using System.ComponentModel.DataAnnotations;

namespace Core.UseCases.EditCashgame
{
    public class EditCashgameRequest
    {
        public string Slug { get; private set; }
        public string DateStr { get; private set; }
        [Required(ErrorMessage = "Please select or enter a location")]
        public string Location { get; private set; }

        public EditCashgameRequest(string slug, string dateStr, string location)
        {
            Slug = slug;
            DateStr = dateStr;
            Location = location;
        }
    }
}