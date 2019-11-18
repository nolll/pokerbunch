using JetBrains.Annotations;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web.Models;

namespace Web.Extensions
{
    public static class CustomHtmlHelpers
    {
        public static IHtmlContent RenderModel(this IHtmlHelper htmlHelper, [AspMvcView] string partialViewName, object model)
        {
            if (model == null)
                return HtmlString.Empty;
            return htmlHelper.Partial(partialViewName, model, null);
        }

        public static IHtmlContent RenderModel(this IHtmlHelper helper, IViewModel model)
        {
            var view = model?.GetView();
            return helper.RenderModel(view, model);
        }
    }
}