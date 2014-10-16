namespace Core.UseCases.ClearCache
{
    public class ClearCacheOutput
    {
        public int DeleteCount { get; private set; }

        public ClearCacheOutput(int deleteCount)
        {
            DeleteCount = deleteCount;
        }
    }
}