using Web.Extensions;

namespace Web.Components.PlayerModels.Badges
{
    public class BadgeModel : Component
    {
        public string Description { get; private set; }
        public string CssClass { get; private set; }
        
        public BadgeModel(string description, bool wasEarned)
        {
            Description = description;
            CssClass = wasEarned ? "icon-check" : "icon-check-empty";
        }
    }
}