using Application.UseCases.AppContext;
using Web.Models.PageBaseModels;

namespace Web.Models.UserModels.Edit
{
    public class EditUserPageModel : AppPageModel
    {
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public string RealName { get; set; }
        public string Email { get; set; }

        public EditUserPageModel(AppContextResult contextResult)
            : base("Edit Profile", contextResult)
        {
        }
    }
}