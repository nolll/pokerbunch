using Web.Extensions;

namespace Web.Components.PlayerModels.Badges
{
    public class BadgeModel : Component
    {
        public string Description { get; }
        public string CssClass { get; }
        
        public BadgeModel(string description, bool wasEarned)
        {
            Description = description;
            CssClass = wasEarned ? "icon-check" : "icon-check-empty";
        }
    }
}