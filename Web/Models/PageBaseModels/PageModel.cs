using Web.Extensions;
using Web.InlineCode;
using Web.Models.MiscModels;
using Web.Settings;

namespace Web.Models.PageBaseModels;

public abstract class PageModel : IViewModel
{
    public string Version { get; }
    public virtual string Layout => ContextLayout.Base;
    public virtual string HtmlCssClass => null;
    public virtual string BodyCssClass => "body-wide";
    public string VueConfigScriptHtml { get; }
    public abstract string BrowserTitle { get; }
    public static string StyleView => "~/Views/Generated/Style.cshtml";
    public static string ScriptView => "~/Views/Generated/Script.cshtml";
    public string FontStyleHtml { get; }

    protected PageModel(AppSettings appSettings)
    {
        var vueConfigScript = new VueConfigScript(appSettings);

        Version = appSettings.Version;
        VueConfigScriptHtml = vueConfigScript.Html;
        FontStyleHtml = new FontStyle().Html;
    }

    public abstract View GetView();
}