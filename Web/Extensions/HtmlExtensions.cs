using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Web.Extensions
{
    public static class CustomHtmlHelpers
    {
        public static MvcHtmlString PartialOrDiscard(this HtmlHelper helper, Component model)
        {
            return model == null ? MvcHtmlString.Empty : helper.Partial(model.ViewName, model);
        }
    }
}