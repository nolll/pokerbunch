namespace Core.Entities
{
    public class SmallBunch
    {
        public string Id { get; }
        public string DisplayName { get; }
        public string Description { get; }

        public SmallBunch(
            string id, 
            string displayName = null,
            string description = null)
        {
            Id = id;
            DisplayName = displayName ?? string.Empty;
            Description = description ?? string.Empty;
        }
    }
}