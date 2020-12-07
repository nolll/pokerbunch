namespace Web.InlineCode
{
    public abstract class InlineScriptHtml : InlineCodeHtml
    {
        protected InlineScriptHtml()
            : base("<script>", "</script>")
        {
        }
    }
}