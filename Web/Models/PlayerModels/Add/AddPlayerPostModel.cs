using System.ComponentModel.DataAnnotations;

namespace Web.Models.PlayerModels.Add
{
    public class AddPlayerPostModel
    {
        [Required(ErrorMessage = "Name can't be empty")]
        public string Name { get; set; }
	}
}