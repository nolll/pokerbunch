using Core.Urls;

namespace Core.UseCases.Report
{
    public class ReportResult
    {
        public Url ReturnUrl { get; private set; }
        
        public ReportResult(Url returnUrl)
        {
            ReturnUrl = returnUrl;
        }
    }
}