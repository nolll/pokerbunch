namespace Core.UseCases
{
    public class UserItem
    {
        public string Name { get; private set; }
        public string Identifier { get; private set; }

        public UserItem(string name, string identifier)
        {
            Name = name;
            Identifier = identifier;
        }
    }
}