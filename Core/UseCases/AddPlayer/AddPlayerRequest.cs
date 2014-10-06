using System.ComponentModel.DataAnnotations;

namespace Core.UseCases.AddPlayer
{
    public class AddPlayerRequest
    {
        public string Slug { get; private set; }
        [Required(ErrorMessage = "Name can't be empty")]
        public string Name { get; private set; }

        public AddPlayerRequest(string slug, string name)
        {
            Slug = slug;
            Name = name;
        }
    }
}