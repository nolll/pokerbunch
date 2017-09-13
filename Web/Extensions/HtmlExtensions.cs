using System.Web.Mvc;
using System.Web.Mvc.Html;
using JetBrains.Annotations;
using Web.Models;

namespace Web.Extensions
{
    public static class CustomHtmlHelpers
    {
        public static MvcHtmlString RenderModel(this HtmlHelper htmlHelper, [AspMvcView] string partialViewName, object model)
        {
            if (model == null)
                return MvcHtmlString.Empty;
            return htmlHelper.Partial(partialViewName, model, null);
        }

        public static MvcHtmlString RenderModel(this HtmlHelper helper, IViewModel model)
        {
            var view = model?.GetView();
            return helper.RenderModel(view, model);
        }
    }
}