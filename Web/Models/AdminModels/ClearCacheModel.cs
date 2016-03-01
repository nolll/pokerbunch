namespace Web.Models.AdminModels
{
    public class ClearCacheModel
    {
        public string Message { get; private set; }

        public ClearCacheModel(int objectCount)
        {
            Message = GetMessage(objectCount);
        }

        private string GetMessage(int objectCount)
        {
            if (objectCount == 0)
                return "The cache contained no objects";
            if (objectCount == 1)
                return "1 object was removed from the cache";
            return $"{objectCount} objects was removed from the cache";
        }
    }
}