namespace Web.InlineCode;

public abstract class InlineStyleHtml : InlineCodeHtml
{
    protected InlineStyleHtml()
        : base("<style type=\"text/css\">", "</style>")
    {
    }
}