using Core.UseCases.ClearCache;

namespace Web.Models.AdminModels
{
    public class ClearCacheModel
    {
        public string Message { get; private set; }

        public ClearCacheModel(ClearCacheOutput clearCacheOutput)
        {
            Message = GetMessage(clearCacheOutput.DeleteCount);
        }

        private string GetMessage(int objectCount)
        {
            if (objectCount == 0)
                return "The cache contained no objects";
            if (objectCount == 1)
                return "1 object was removed from the cache";
            return string.Format("{0} objects was removed from the cache", objectCount);
        }
    }
}