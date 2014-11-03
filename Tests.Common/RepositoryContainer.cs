namespace Tests.Common
{
    public class RepositoryContainer
    {
        public FakeBunchRepository Bunch { get; private set; }

        public RepositoryContainer()
        {
            Bunch = new FakeBunchRepository();
        }
    }
}