using System.ComponentModel.DataAnnotations;
using Web.Annotations;

namespace Web.Models.HomegameModels.Join
{
    public class JoinHomegamePostModel
    {
        [Required(ErrorMessage = "Code can't be empty")]
        public string Code { get; [UsedImplicitly] set; }
    }
}