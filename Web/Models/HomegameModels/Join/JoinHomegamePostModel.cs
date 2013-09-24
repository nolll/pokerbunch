using System.ComponentModel.DataAnnotations;

namespace Web.Models.HomegameModels.Join
{
    public class JoinHomegamePostModel
    {
        [Required(ErrorMessage = "Code can't be empty")]
        public string Code { get; set; }
    }
}