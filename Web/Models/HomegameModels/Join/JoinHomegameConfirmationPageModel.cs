using Application.Urls;
using Application.UseCases.AppContext;
using Web.Models.PageBaseModels;

namespace Web.Models.HomegameModels.Join
{
    public class JoinHomegameConfirmationPageModel : AppPageModel
    {
        public string BunchName { get; set; }
        public Url BunchUrl { get; set; }

        public JoinHomegameConfirmationPageModel(AppContextResult contextResult)
            : base("Welcome", contextResult)
        {
        }
    }
}