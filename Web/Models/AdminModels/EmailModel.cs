using Core.UseCases.ClearCache;
using Core.UseCases.TestEmail;

namespace Web.Models.AdminModels
{
    public class EmailModel
    {
        public string Email { get; private set; }

        public EmailModel(TestEmailResult result)
        {
            Email = result.Email;
        }
    }

    public class ClearCacheModel
    {
        public string Message { get; private set; }

        public ClearCacheModel(ClearCacheOutput clearCacheOutput)
        {
            Message = GetMessage(clearCacheOutput.ObjectCount);
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