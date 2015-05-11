using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Web.Extensions
{
    public static class CustomHtmlHelpers
    {
        public static MvcHtmlString PartialOrDiscard(this HtmlHelper helper, IViewModel model)
        {
            return model == null ? MvcHtmlString.Empty : helper.Partial(model.ViewName, model);
        }
    }

    public interface IViewModel
    {
        string ViewName { get; }
    }
}