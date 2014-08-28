using Application.Urls;
using Application.UseCases.AppContext;
using Web.Models.PageBaseModels;

namespace Web.Models.HomegameModels.Join
{
    public class JoinBunchConfirmationPageModel : AppPageModel
    {
        public string BunchName { get; set; }
        public Url BunchUrl { get; set; }

        public JoinBunchConfirmationPageModel(AppContextResult contextResult)
            : base("Welcome", contextResult)
        {
        }
    }
}