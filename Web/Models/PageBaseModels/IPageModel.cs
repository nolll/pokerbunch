namespace Web.Models.PageBaseModels
{
    public interface IPageModel
    {
        PageProperties PageProperties { get; }
        string BrowserTitle { get; }
    }
}