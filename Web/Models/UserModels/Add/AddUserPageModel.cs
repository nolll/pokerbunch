using Application.UseCases.AppContext;
using Web.Models.PageBaseModels;

namespace Web.Models.UserModels.Add
{
    public class AddUserPageModel : PageModel
    {
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }

        public AddUserPageModel(AppContextResult contextResult)
            : base("Register", contextResult)
        {
        }
    }
}