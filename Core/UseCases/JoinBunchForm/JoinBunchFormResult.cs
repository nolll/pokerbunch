namespace Core.UseCases.JoinBunchForm
{
    public class JoinBunchFormResult
    {
        public string BunchName { get; private set; }

        public JoinBunchFormResult(string bunchName)
        {
            BunchName = bunchName;
        }
    }
}