using System.ComponentModel.DataAnnotations;

namespace Web.Models.PlayerModels.Add
{
    public class AddPlayerPostModel
    {
        [Required]
        public string Name { get; set; }
	}
}