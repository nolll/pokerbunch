using Application.UseCases.AppContext;
using Web.Models.PageBaseModels;

namespace Web.Models.HomegameModels.Join
{
    public class JoinHomegamePageModel : PageModel
    {
        public string Name { get; set; }
        public string Code { get; set; }

        public JoinHomegamePageModel(AppContextResult contextResult)
            : base("Join Bunch", contextResult)
        {
        }
    }
}