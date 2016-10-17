namespace Core.Exceptions
{
    public class BunchNotFoundException : NotFoundException
    {
        public BunchNotFoundException(string bunchId)
            : base(GetMessage(bunchId))
        {
        }

        private static string GetMessage(string bunchId)
        {
            return $"Bunch not found: {bunchId}";
        }
    }
}