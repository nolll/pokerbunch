using System.Collections.Generic;
using System.Linq;
using Web.Extensions;

namespace Web.Models.ErrorModels
{
    public class ErrorListModel : IViewModel
    {
        public IEnumerable<string> Messages { get; }
        public bool HasErrors => Messages.Any();

        public ErrorListModel(IEnumerable<string> messages)
        {
            Messages = messages ?? new List<string>();
        }

        public View GetView()
        {
            return new View("~/Views/Forms/Errors.cshtml");
        }
    }
}