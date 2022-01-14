using System.Linq;

namespace Web.InlineCode;

public class FontStyle : InlineStyleHtml
{
    private static string Source => @"
@font-face {
    font-family: 'FontAwesome';
    src: url('/dist/fonts/fontawesome-webfont.eot?v=3.0.1');
    src: url('/dist/fonts/fontawesome-webfont.eot?#iefix&v=3.0.1') format('embedded-opentype'), url('/dist/fonts/fontawesome-webfont.woff?v=3.0.1') format('woff'), url('/dist/fonts/fontawesome-webfont.ttf?v=3.0.1') format('truetype');
    font-weight: normal;
    font-style: normal;
}";

    protected override string Content { get; }

    public FontStyle()
    {
        Content = string.Join(" ", Source.Trim().Split("\r\n").Select(o => o.Trim()));
    }
}