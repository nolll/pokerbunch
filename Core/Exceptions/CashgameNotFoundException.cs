namespace Core.Exceptions
{
    public class CashgameNotFoundException : NotFoundException
    {
        public CashgameNotFoundException(string slug, string dateStr)
            : base(GetMessage(slug, dateStr))
        {
        }

        private static string GetMessage(string slug, string dateStr)
        {
            return string.Format("Cashgame not found: bunch = '{0}', date = '{1}'", slug, dateStr);
        }
    }
}