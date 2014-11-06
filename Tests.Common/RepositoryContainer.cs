namespace Tests.Common
{
    public class RepositoryContainer
    {
        public FakeBunchRepository Bunch { get; private set; }
        public FakeUserRepository User { get; private set; }
        public FakeCheckpointRepository Checkpoint { get; private set; }
        public FakePlayerRepository Player { get; private set; }

        public RepositoryContainer()
        {
            Bunch = new FakeBunchRepository();
            User = new FakeUserRepository();
            Checkpoint = new FakeCheckpointRepository();
            Player = new FakePlayerRepository();
        }
    }
}