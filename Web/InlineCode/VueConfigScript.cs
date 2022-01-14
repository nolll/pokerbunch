using System;
using System.Security.Cryptography;
using System.Text;
using Web.Settings;

namespace Web.InlineCode;

public class VueConfigScript : InlineScriptHtml
{
    protected override string Content { get; }

    public VueConfigScript(AppSettings appSettings)
    {
        var apiUrl = appSettings.Urls.ApiUri.AbsoluteUri.TrimEnd('/');
        Content = $"window.vueConfig = {{ apiUrl: '{apiUrl}' }};"; ;
    }
}