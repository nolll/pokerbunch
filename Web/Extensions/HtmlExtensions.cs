using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Web.Extensions
{
    public static class CustomHtmlHelpers
    {
        public static MvcHtmlString Component(this HtmlHelper helper, Component model)
        {
            return model == null ? MvcHtmlString.Empty : helper.Partial(model.ViewName, model);
        }
    }
}