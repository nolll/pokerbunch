using Application.UseCases.BunchContext;
using Web.Models.PageBaseModels;

namespace Web.Models.PlayerModels.Add
{
    public class AddPlayerPageModel : PageModel
    {
        public string Name { get; set; }

        public AddPlayerPageModel(BunchContextResult contextResult)
            : base("Add Player", contextResult)
        {
        }
    }
}