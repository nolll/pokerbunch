namespace Application.UseCases.AddBunchForm
{
    public class TimeZoneItem
    {
        public string Id { get; private set; }
        public string Name { get; private set; }

        public TimeZoneItem(string id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}