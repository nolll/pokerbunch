using Application.Urls;
using Web.Models.UrlModels;

namespace Web.Models.CashgameModels.Action
{
    public class CheckpointModel
    {
        public string Description { get; set; }
        public string Stack { get; set; }
        public string Timestamp { get; set; }
        public bool ShowLink { get; set; }
        public Url EditUrl { get; set; }
    }
}