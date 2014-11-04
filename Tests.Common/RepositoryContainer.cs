namespace Tests.Common
{
    public class RepositoryContainer
    {
        public FakeBunchRepository Bunch { get; private set; }
        public FakeUserRepository User { get; private set; }

        public RepositoryContainer()
        {
            Bunch = new FakeBunchRepository();
            User = new FakeUserRepository();
        }
    }
}