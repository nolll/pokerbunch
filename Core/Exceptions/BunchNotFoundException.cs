namespace Core.Exceptions
{
    public class BunchNotFoundException : NotFoundException
    {
        public BunchNotFoundException(string slug)
            : base(GetMessage(slug))
        {
        }

        public BunchNotFoundException(int id)
            : base(GetMessage(id))
        {
        }

        private static string GetMessage(string slug)
        {
            return string.Format("Bunch not found: slug = '{0}'", slug);
        }

        private static string GetMessage(int id)
        {
            return string.Format("Bunch not found: id = {0}", id);
        }
    }
}